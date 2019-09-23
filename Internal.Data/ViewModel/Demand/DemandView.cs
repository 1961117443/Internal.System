using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{
    public class DemandView
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public string id { get; set; }
        /// <summary>
        /// 需求编号
        /// </summary>
        public string billCode { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public string inputDate { get; set; }
        /// <summary>
        /// 理论交期
        /// </summary>
        public string theoryDate { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public string plannedCompletionDate { get; set; }
        /// <summary>
        /// 提出日期
        /// </summary>
        public string suggestDate { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string priority { get; set; }
        /// <summary>
        /// 软件版本
        /// </summary>
       // public int versionNumber { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string clientRelationship { get; set; }
        /// <summary>
        /// 实施员
        /// </summary>
        public string implement { get; set; }
        /// <summary>
        /// 需求提出者
        /// </summary>
        public string demander { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 所属模块
        /// </summary>
        public string moduleName { get; set; }
        /// <summary>
        /// 需求描述
        /// </summary>
        public string describe { get; set; }

    }
}
