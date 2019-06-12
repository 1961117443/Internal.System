using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internal.Common.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Internal.App.Controllers
{
    public abstract class BaseController : Controller
    {
        #region 返回自动定义的数据格式
        protected IActionResult ApiResult(string msg)
        {
            ResultMessage result = new ResultMessage()
            {
                Message = msg,
                Status = 0
            };
            return Json(result);
        }
        protected IActionResult ApiResult<T>(T data, string msg = "")
        {
            ResultMessage<T> result = new ResultMessage<T>()
            {
                Message = msg,
                Status = 0,
                Data = data
            };
            return Json(result);
        } 
        #endregion
    }
}