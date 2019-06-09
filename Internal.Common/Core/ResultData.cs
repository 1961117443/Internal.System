using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    public class ResultData
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ResultData()
        {
            Status = 0;
        }
    }

    public class ResultData<T>:ResultData
    { 
        public T Data { get; set; }
    }
}
