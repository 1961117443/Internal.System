﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internal.Data.Entity
{
    public class Demand
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillCode { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime RecordDate { get; set; }
        /// <summary>
        /// 提出者
        /// </summary>
        public string Presenter { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string Maker { get; set; }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime MakeDate { get; set; }

        /// <summary>
        /// 需求描述
        /// </summary>
        public string Describe { get; set; }

        [SqlSugar.SugarColumn(IsIgnore = true)]
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get;set;}
    }
}
