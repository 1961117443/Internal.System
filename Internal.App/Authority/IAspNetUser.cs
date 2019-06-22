using Internal.Common.Core;
using Internal.Common.Helpers;
using Internal.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internal.App.Authority
{
    
    public interface IAspNetUser
    {
        string Id { get;}

        string Name { get;} 
        bool IsLogin { get; }
        string AccessToken { get; }
    }

    /// <summary>
    /// 解析token,获取用户信息
    /// </summary>
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JwtToken jwtToken;
        private  TokenModelJwt _tokenModel;
        private  string accessToken;

        public AspNetUser(IHttpContextAccessor httpContextAccessor, JwtToken jwtToken)
        { 
            //if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            //{
            //    var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            //    if (token.StartsWith("Bearer "))
            //    {
            //        token = token.Substring("Bearer ".Length);
            //    }
            //    var tokenModel = jwtToken.SerializeJwt(token);
            //    this.Id = tokenModel.Uid;
            //    this.Name =tokenModel.Name;
            //}

            this.httpContextAccessor = httpContextAccessor;
            this.jwtToken = jwtToken;  
        }

        public string Id
        {
            get
            {
                return IsLogin ? _tokenModel.Uid : Guid.Empty.ToString();
            }
        }
        public string Name
        {
            get


            {
                return IsLogin ? _tokenModel.Name : "";

            }
        }

        public bool IsLogin
        {
            get
            {
                if (_tokenModel==null)
                {
                    try
                    {
                        _tokenModel = jwtToken.SerializeJwt(AccessToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                   
                }
                return _tokenModel != null;
            }
        }

        public string AccessToken 
        {
            get
            {
                if (accessToken.IsEmpty())
                {
                    if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                    {
                        var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                        if (token.StartsWith("Bearer "))
                        {
                            accessToken= token.Substring("Bearer ".Length);
                        } 
                    }
                }
                return accessToken;
            } 
         
        }
    }
}
