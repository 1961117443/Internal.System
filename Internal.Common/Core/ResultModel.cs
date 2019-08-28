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
}
