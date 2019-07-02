using Internal.Common.Core;
using Internal.Common.Helpers;
using Microsoft.AspNetCore.Http;
using System;

namespace Internal.App.Authority
{
    /// <summary>
    /// 解析token,获取用户信息
    /// </summary>
    public class AspNetUser : IAspNetUser
    {
        #region 属性字段
        private static readonly string AuthorizationHeader = "Authorization";
        private static readonly string Bearer = "Bearer ";

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JwtToken jwtToken;
        private TokenModelJwt _tokenModel;
        private string authToken; 
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="jwtToken"></param>
        public AspNetUser(IHttpContextAccessor httpContextAccessor, JwtToken jwtToken)
        {   
            this.httpContextAccessor = httpContextAccessor;
            this.jwtToken = jwtToken;  
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id
        {
            get
            {
                return IsLogin ? _tokenModel.Uid : Guid.NewGuid().ToString();
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name
        {
            get  
            {
                return IsLogin ? _tokenModel.Name : "(未登录用户)";

            }
        }

        /// <summary>
        /// 判断是否登陆了
        /// 就是验证authToken 合法，有没有过期
        /// </summary>
        public bool IsLogin
        {
            get
            {
                if (_tokenModel==null)
                {
                    try
                    {
                        _tokenModel = jwtToken.SerializeJwt(AuthToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                   
                }
                return _tokenModel != null;
            }
        }

        /// <summary>
        /// 用户凭据
        /// </summary>
        public string AuthToken 
        {
            get
            {
                if (authToken.IsEmpty())
                {
                    if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey(AuthorizationHeader))
                    {
                        var token = httpContextAccessor.HttpContext.Request.Headers[AuthorizationHeader].ToString();
                        if (token.StartsWith(Bearer))
                        {
                            authToken= token.Substring(Bearer.Length);
                        } 
                    }
                }
                return authToken;
            } 
         
        }
    }
}
