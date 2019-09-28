﻿using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{

    public class DemandViewModel
    {/// <summary>
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
        public string CustomerIDCode { get; set; }
        public string CustomerIDName { get; set; }

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
    }
}
