using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internal.App.Authority;
using Internal.Common.Core; 
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

        public AccountController(IUserService userService, JwtToken jwtToken)
        {
            this._userService = userService;
            this._jwtToken = jwtToken;
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
        public async Task<IActionResult> Login(string userCode,string passWord)
        {
            UserInfo userInfo = new UserInfo();
            ResultModel<string> resultModel = new ResultModel<string>();
            TokenModelJwt tokenModel = new TokenModelJwt()
            {
                Uid = 12345,
                Role = "Admin",
                Work = "超级管理员"
            };
            var token = _jwtToken.IssueJwt(tokenModel);
            resultModel.Data = token;
            return Ok(resultModel);
        }

        /// <summary>
        /// 刷新Access_Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("refresh_token")] 
        public async Task<string> RefreshAccessToken(string access_token)
        {
            var tokenModel = _jwtToken.SerializeJwt(access_token); 
            return this._jwtToken.IssueJwt(tokenModel); 
        }
    }
}