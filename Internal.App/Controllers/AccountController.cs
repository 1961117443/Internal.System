using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Internal.App.Authority;
using Internal.Common.Core;
using Internal.Common.Helpers;
using Internal.Data.Entity;
using Internal.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internal.App.Controllers
{
    /// <summary>
    /// 账号管理
    /// </summary>
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtToken _jwtToken;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IUserService userService, JwtToken jwtToken,IHttpContextAccessor httpContextAccessor)
        {
            this._userService = userService;
            this._jwtToken = jwtToken;
            this._httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 登陆操作
        /// 登陆成功后颁发access_token
        /// </summary>
        /// <param name="userCode">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string userCode,string passWord,[FromServices]IAspNetUser aspNetUser)
        {
            ResultModel<string> resultModel = new ResultModel<string>();
            if (aspNetUser!=null && aspNetUser.IsLogin)
            {
                resultModel.Data = aspNetUser.AccessToken;
                return Ok(resultModel);
            }
            var users = await this._userService.Query(w => w.UserCode == userCode);
            if (!users.Any())
            {
                resultModel.Message = "账号不存在";
                resultModel.Status = 20001;
            }
            else
            { 
                var user = users.FirstOrDefault(w => w.UserCode == userCode && w.PassWord == MD5($"{passWord}{w.ID}"));
                if (user==null)
                {
                    resultModel.Message = "密码错误"; ;
                    resultModel.Status = 20002;
                }
                else
                {

                    TokenModelJwt tokenModel = new TokenModelJwt()
                    {
                        Uid = user.ID.ToString(),
                        Name = user.UserName,
                        //Work = "超级管理员"
                    };
                    var token = _jwtToken.IssueJwt(tokenModel);
                    var u = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                    resultModel.Data = token;
                } 
            } 
            
            return Ok(resultModel);
        }

        /// <summary>
        /// 刷新Access_Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("refreshtoken")] 
        public async Task<IActionResult> RefreshToken(string access_token)
        {
            var user = this._httpContextAccessor.HttpContext.User;
            var tokenModel = _jwtToken.SerializeJwt(access_token);
            var res= new ResultModel() { Message = this._jwtToken.IssueJwt(tokenModel) };
            return Ok(res);
        }


        [NonAction]
        public string MD5(string str)
        {
            byte[] bytes = MD5(System.Text.Encoding.ASCII.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            foreach (byte num in bytes)
            {
                sb.AppendFormat("{0:x2}", num);
            }
            return sb.ToString();
        }
        [NonAction]
        public byte[] MD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }
    }
}