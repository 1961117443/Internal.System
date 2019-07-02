using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{
    /// <summary>
    /// 需求列表的卡片模型
    /// </summary>
    public class DemandCardModel
    {
        /// <summary>
        /// 需求主键
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 需求编号
        /// </summary>
        public string BillCode { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime InputDate { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; } 
        /// <summary>
        /// 需求描述
        /// </summary>
        public string Describe { get; set; }


        /// <summary>
        /// 审核人
        /// </summary>
        public string Audit { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditDate { get; set; }
        /// <summary>
        /// 拒批人
        /// </summary>
        public string Rejector { get; set; }
        /// <summary>
        /// 拒批时间 
        /// </summary>
        public DateTime RejectDate { get; set; }
    }
}
