using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.ModelResult
{
    /// <summary>
    /// 表格数据的返回类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableReult<T>
    {
        public int Total { get; set; }
        public List<T> TableList { get; set; }
    }
}
