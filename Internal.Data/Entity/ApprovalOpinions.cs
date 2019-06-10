using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{ 
    /// <summary>
    /// 审批意见
    /// </summary>
    public class ApprovalOpinions
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 需求id
        /// </summary>
        public Guid DemandID { get; set; }
        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime ApprovalDate { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string ApprovalOper { get; set; }
    }
}
