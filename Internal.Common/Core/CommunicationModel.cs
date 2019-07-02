using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    /// <summary>
    /// 服务层与应用层之间的通讯
    /// </summary>
    /// <typeparam name="T">返回的数据模型</typeparam>
    public class CommunicationModel<T>
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
