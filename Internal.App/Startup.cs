using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Internal.IRepository;
using Internal.IService;
using Internal.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using System.Linq.Expressions;
using Internal.Data.Entity;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Internal.App.Filters;
using Internal.App.Authority;
using Internal.App.Options;
using Microsoft.AspNetCore.Http;
using Internal.Common.Cache;
using Internal.Common.Options;

namespace Internal.App
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 注入自己的服务 
            services.Configure<TokenOptions>(Configuration.GetSection("AuthTokenOptions")); 
            services.AddSingleton(typeof(JwtToken));
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            #endregion
            #region 配置授权认证
            services
                .AddAuthorization(
                    opt =>
                    { 
                        opt.AddPolicy("CustomPermission", ap => ap.AddRequirements(new PermissionRequirement()));
                    })
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    // RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                    // ValidateAudience = true,
                    // ValidateIssuer = true, 
                    // ValidateIssuerSigningKey = false,
                    // 是否要求Token的Claims中必须包含Expires
                    // RequireExpirationTime = true,
                    // 允许的服务器时间偏移量
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    // ValidateLifetime = true
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = Configuration.GetSection("AuthTokenOptions:Issuer").Value,
                        ValidIssuer = Configuration.GetSection("AuthTokenOptions:Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AuthTokenOptions:Secret").Value)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                    };

                });
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            #endregion

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    //使用默认方式，不更改元数据的key的大小写
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            #region 注入 HttpContext 服务 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.AddScoped<IAspNetUser, AspNetUser>();
            #endregion

            #region AutoMapper 先注册autoMapper 在使用autofac框架托管
            services.AddAutoMapper(Assembly.Load("Internal.Data"));
            #endregion

            #region Swagger

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "内部系统 API",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Internal.System", Email = "", Url = "" }
                });

                var xmlPath = Path.Combine(basePath, "Internal.App.xml");//这个就是刚刚配置的xml文件名
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                }
                if (File.Exists(xmlPath))
                { 
                    c.IncludeXmlComments(Path.Combine(basePath, "Internal.Data.xml"));
                }

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Internal.App", new string[] { } }, };
                c.AddSecurityRequirement(security);
                //方案名称“Blog.Core”可自定义，上下一致即可
                c.AddSecurityDefinition("Internal.App", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion

            });
            #endregion
            #region CORS 跨域
            //跨域第二种方法，声明策略，记得下边app中配置
            services.AddCors(c =>
            {
                //↓↓↓↓↓↓↓注意正式环境不要使用这种全开放的处理↓↓↓↓↓↓↓↓↓↓
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()//允许任何源
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头
                    .AllowCredentials();//允许cookie
                });
                //↑↑↑↑↑↑↑注意正式环境不要使用这种全开放的处理↑↑↑↑↑↑↑↑↑↑
                List<string> os = new List<string>();
                var origins = Configuration.GetSection("AllowAnyOrigins");
                if (origins!=null)
                {
                    foreach (var cfg in origins.GetChildren())
                    {
                        os.Add($"{cfg.Value}");                        
                    }
                } 
                //一般采用这种方法
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins(os.ToArray())//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            //跨域 注意下边 Configure方法 中进行配置
            //services.AddCors();
            #endregion 
            #region AutoFac

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            //注册要通过反射创建的组件
            //builder.RegisterType<DemandService>().As<IDemandService>();
            var assemblysServices = Assembly.Load("Internal.Service");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。
            var assemblysRepository = Assembly.Load("Internal.Repository.SqlServer");//模式是 Load(解决方案名)
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("LimitRequests");

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion


            #region 启用授权认证
            app.UseAuthentication();
            #endregion

            app.UseMvc();
        }
    }
}
