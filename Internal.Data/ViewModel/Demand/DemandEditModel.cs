using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{
    /// <summary>
    /// 需求编辑模型
    /// </summary>
    public class DemandEditModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; } 
        /// <summary>
        /// 客户id
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime RecordDate { get; set; }  
        /// <summary>
        /// 需求描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 提出者
        /// </summary>
        public string Presenter { get; set; }

    }
}
