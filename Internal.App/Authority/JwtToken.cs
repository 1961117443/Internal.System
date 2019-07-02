using Internal.App.Options;
using Internal.Common.Core;
using Internal.Common.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Internal.App.Authority
{
    public class JwtToken
    {
        /// <summary>
        /// token设置值
        /// </summary>
        private readonly TokenOptions options;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JwtToken(IOptions<TokenOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            this.options = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public string IssueJwt(TokenModelJwt tokenModel)
        {
            string iss = options.Issuer;  // Appsettings.app(new string[] { "Audience", "Issuer" });
            string aud = options.Audience;  //Appsettings.app(new string[] { "Audience", "Audience" });
            //密钥的长度最少是16个字符
            string secret = options.Secret; // Appsettings.app(new string[] { "Audience", "Secret" });

            //var claims = new Claim[] //old
            var claims = new List<Claim>
                {
                    //下边为Claim的默认配置
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，目前是过期100秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(3600)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud), 
                
                //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
               };

            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
            if (!tokenModel.Role.IsEmpty())
            {
                claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            }
            claims.Add(new Claim(ClaimTypes.Name, tokenModel.Name));


            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
           // this.httpContextAccessor.HttpContext.User
            //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            // await this.httpContextAccessor.HttpContext.Authentication.SignInAsync(JwtBearerDefaults.AuthenticationScheme, claimsPrincipal);

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            //密钥的长度最少是16个字符，不然会报错
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr); 
            object role,name;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtToken.Payload.TryGetValue(ClaimTypes.Name, out name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Uid= jwtToken.Id,
                Name = name!=null ? name.ToString():"", 
                Role = role != null ? role.ToString() : "",
            };
            return tm;
        }
    }
}
