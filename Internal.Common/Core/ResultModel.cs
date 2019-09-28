using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    public class ResultModel
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public ResultModel()
        {
            Status = ApiResultStatus.OK;
        }
    }

    public class ResultModel<T>
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }

        public string Message { get; set; }

        public ResultModel()
        {
            Success = true;
            Status = ApiResultStatus.OK;

        }
    }

    /// <summary>
    /// api数据返回格式
    /// </summary>
    /// <typeparam name="T">数据对象</typeparam>
    //public class ApiResult<T>
    //{
    //    /// <summary>
    //    /// 错误码
    //    /// </summary>
    //    public int error_code { get; set; }
    //    /// <summary>
    //    /// 错误信息
    //    /// </summary>
    //    public string msg { get; set; }
    //    /// <summary>
    //    /// 数据对象
    //    /// </summary>
    //    public T data { get; set; }

    //    public ApiResult()
    //    {
    //        error_code = 0;
    //        msg = string.Empty;
    //    }

    //    public ApiResult(T res):this()
    //    {
    //        this.data = res;
    //    } 
    //}
}
