using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data
{
    public class ApiResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int error_code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; }

        public ApiResult()
        {
            error_code = 0;
            msg = string.Empty;
        }
    }
    /// <summary>
    /// api数据返回格式
    /// </summary>
    /// <typeparam name="T">数据对象</typeparam>
    public class ApiResult<T> : ApiResult
    { 
        /// <summary>
        /// 数据对象
        /// </summary>
        public T Data { get; set; } 

        public ApiResult(T res) :this()
        {
            this.Data = res;
        }

        public ApiResult() : base()
        {
        }
    }

    /// <summary>
    /// 表格返回类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiTableResult<T>:ApiResult<IEnumerable<T>>
    {
        public ApiTableResult()
        {

        } 
        
        public ApiTableResult(IEnumerable<T> data)
        {
            this.Data = data;
        }
    }
        
}
