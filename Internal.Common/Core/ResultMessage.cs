using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    public class ResultModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ResultModel()
        {
            Status = ApiResultStatus.OK;
        }
    }

    public class ResultModel<T>
    {
        public int Status { get; set; }
        public T Data { get; set; }

        public ResultModel()
        {
            Status = ApiResultStatus.OK;

        }
    }
}
