using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{
    /// <summary>
    /// 需求查询实体
    /// </summary>
    public class DemandQueryModel
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string BillCode { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime InputDate { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerIDName { get; set; }
    }
}
