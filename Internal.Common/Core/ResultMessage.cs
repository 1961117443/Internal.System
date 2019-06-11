using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    public class ResultMessage
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ResultMessage()
        {
            Status = 0;
        }
    }

    public class ResultMessage<T>:ResultMessage
    { 
        public T Data { get; set; }
    }
}
