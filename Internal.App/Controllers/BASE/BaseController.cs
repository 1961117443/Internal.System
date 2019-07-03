using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internal.Common.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Internal.App.Controllers
{
    /// <summary>
    /// 需要权限认证的基类
    /// </summary>
    [Authorize("CustomPermission")]
    public abstract class BaseController : Controller
    {
        #region 返回自动定义的数据格式
        protected IActionResult ApiResult(string msg)
        {
            ResultModel result = new ResultModel()
            {
                Message = msg,
                Status = 0
            };
            return Json(result);
        }
        protected IActionResult ApiResult<T>(T data)
        {
            ResultModel<T> result = new ResultModel<T>()
            { 
                Status = 0,
                Data = data
            };
            return Json(result);
        } 
        #endregion
    }
}