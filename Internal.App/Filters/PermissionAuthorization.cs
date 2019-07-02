using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Internal.App.Authority;
using Internal.Common.Helpers;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Internal.App.Filters
{
    //public class PermissionItem
    //{
    //    public string RoleID { get; set; }
    //    public string PermissionID { get; set; }
    //}
    
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 访问次数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 拥有的权限
        /// </summary>
        public List<SysPermission> Permissions { get; set; }
        public string ClaimType { get; internal set; }

        /// <summary>
        /// 是否已经初始化权限
        /// 没有初始化的话,获取所有的权限
        /// </summary>
        public bool InitAuth { get; set; }
    }
    /// <summary>
    /// 授权认证过滤器
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private static readonly object lockRequirement = new object();
        private readonly IAspNetUser aspNetUser;
        private readonly ISysPermissionService permissionService;

        public PermissionAuthorizationHandler(IAuthenticationSchemeProvider schemes, IAspNetUser aspNetUser, ISysPermissionService permissionService)
        {
            Schemes = schemes;
            this.aspNetUser = aspNetUser;
            this.permissionService = permissionService;
        }

        public IAuthenticationSchemeProvider Schemes { get; }

        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            return base.HandleAsync(context); 
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            
            //从AuthorizationHandlerContext转成HttpContext，以便取出表头信息
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();
            var method = httpContext.Request.Method.ToLower();

            //判断请求是否停止
            var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name) as IAuthenticationRequestHandler;
                if (handler != null && await handler.HandleRequestAsync())
                {
                    context.Fail();
                    return;
                }
            }
            //判断请求是否拥有凭据，即有没有登录
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                //result?.Principal不为空即登录成功
                if (result?.Principal != null)
                {

                    httpContext.User = result.Principal;
                    //权限中是否存在请求的url
                    if (await permissionService.HasAuth(questUrl, method, aspNetUser.Id))
                    {
                        context.Succeed(requirement);
                    }
                    //if (requirement.Permissions.GroupBy(g => g.Url).Where(w => w.Key?.ToLower() == questUrl).Count() > 0)
                    //{
                    //    // 获取当前用户的角色信息
                    //    var currentUserRoles = (from item in httpContext.User.Claims
                    //                            where item.Type == requirement.ClaimType
                    //                            select item.Value).ToList();


                    //    //验证权限
                    //    if (currentUserRoles.Count <= 0 || requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role) && w.Url.ToLower() == questUrl).Count() <= 0)
                    //    {

                    //        context.Fail();
                    //        return;
                    //    }
                    //}
                    else
                    {
                        context.Fail();
                        return;

                    }
                    //判断过期时间
                    string exp = httpContext.User.Claims.SingleOrDefault(s => s.Type == JwtRegisteredClaimNames.Exp)?.Value;
                    long time;
                    if (!exp.IsEmpty() && long.TryParse(exp,out time) && (DateTimeOffset.FromUnixTimeSeconds(time).ToLocalTime()) >= DateTime.Now)
                    {  
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                        return;
                    }
                    return;
                }
            }
            //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
            if (/*!questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) &&*/ (!httpContext.Request.Method.Equals("POST")
               || !httpContext.Request.HasFormContentType))
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }
}
