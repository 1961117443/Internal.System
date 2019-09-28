using Internal.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internal.App.Filters
{
    public enum RecordLockType
    {
        Lock,
        UnLock
    }

    /// <summary>
    /// 锁定记录拦截器
    /// </summary>
    public class RecordLockAttribute: ActionFilterAttribute
    {
        public RecordLockType Action { get; set; }
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        { 
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

    }
}
