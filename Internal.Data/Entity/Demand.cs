using Internal.Common.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internal.Data.Entity
{
    /// <summary>
    /// NeedConfirm 
    /// <summary>
    [SugarTable("NeedConfirm")]
    [FreeSql.DataAnnotations.Table(Name = "NeedConfirm")]
    public partial class Demand: BaseModel<Guid>
    {
        /// <summary>
        /// RowNo
        /// </summary>
        public int RowNo { get; set; }
        /// <summary>
        /// Appointees
        /// <summary>
        public Guid Appointees { get; set; }
        /// <summary>
        /// Audit
        /// </summary>
        public string Audit { get; set; }
        /// <summary>
        /// AuditDate
        /// <summary>
        public DateTime? AuditDate { get; set; }
        /// <summary>
        /// AutoID
        /// <summary>
        [FreeSql.DataAnnotations.Column(IsIdentity = true)]
        public int AutoID { get; set; }
        /// <summary>
        /// BillCode
        /// <summary>
        public string BillCode { get; set; }
        /// <summary>
        /// BillType
        /// <summary>
        public string BillType { get; set; }
        /// <summary>
        /// ClientAdminSign
        /// <summary>
        public string ClientAdminSign { get; set; }
        /// <summary>
        /// ClientName
        /// <summary>
        public Guid ClientName { get; set; }
        /// <summary>
        /// DemandDescribe
        /// <summary>
        public string DemandDescribe { get; set; }
        /// <summary>
        /// DemandDetail
        /// <summary>
        public string DemandDetail { get; set; }
        /// <summary>
        /// Demander
        /// <summary>
        public string Demander { get; set; }
        /// <summary>
        /// FirstAuditDate
        /// <summary>
        public DateTime? FirstAuditDate { get; set; }
        /// <summary>
        /// FunctionEffect
        /// <summary>
        public string FunctionEffect { get; set; }
        /// <summary>
        /// Implement
        /// <summary>
        public Guid Implement { get; set; }
        /// <summary>
        /// InputDate
        /// <summary>
        public DateTime? InputDate { get; set; }
        /// <summary>
        /// MakeDate
        /// <summary>
        public DateTime? MakeDate { get; set; }
        /// <summary>
        /// Maker
        /// <summary>
        public string Maker { get; set; }
        /// <summary>
        /// Mender
        /// <summary>
        public string Mender { get; set; }
        /// <summary>
        /// ModifyDate
        /// <summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// Priority
        /// <summary>
        public string Priority { get; set; }
        /// <summary>
        /// ProjectMangerSign
        /// <summary>
        public string ProjectMangerSign { get; set; }
        /// <summary>
        /// QQ
        /// <summary>
        public string QQ { get; set; }
        /// <summary>
        /// SignDate
        /// <summary>
        public DateTime? SignDate { get; set; }
        /// <summary>
        /// States
        /// <summary>
        public int? States { get; set; }
        /// <summary>
        /// SuggestDate
        /// <summary>
        public DateTime? SuggestDate { get; set; }
        /// <summary>
        /// TaskCode
        /// <summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// Telephone
        /// <summary>
        public string Telephone { get; set; }
        /// <summary>
        /// TheoryDate
        /// <summary>
        public DateTime? TheoryDate { get; set; }
        /// <summary>
        /// VersionNumber
        /// <summary>
        public Guid VersionNumber { get; set; }
        /// <summary>
        /// Describe
        /// <summary>
        public string Describe { get; set; }
        /// <summary>
        /// CloseUser
        /// <summary>
        public string CloseUser { get; set; }
        /// <summary>
        /// CloseDate
        /// <summary>
        public DateTime? CloseDate { get; set; }
        /// <summary>
        /// PlannedCompletionDate
        /// <summary>
        public DateTime? PlannedCompletionDate { get; set; }
        /// <summary>
        /// ClientRelationship
        /// <summary>
        public string ClientRelationship { get; set; }
        /// <summary>
        /// FinishOper
        /// <summary>
        public string FinishOper { get; set; }
        /// <summary>
        /// FinishDate
        /// <summary>
        public DateTime? FinishDate { get; set; }
        /// <summary>
        /// Temporary
        /// <summary>
        public string Temporary { get; set; }
        /// <summary>
        /// TemporaryDate
        /// <summary>
        public DateTime? TemporaryDate { get; set; }
        /// <summary>
        /// TaskValue
        /// <summary>
        public int? TaskValue { get; set; }
        /// <summary>
        /// IsPendingPrice
        /// <summary>
        public bool? IsPendingPrice { get; set; }
        /// <summary>
        /// ConfirmType
        /// <summary>
        public string ConfirmType { get; set; }
        /// <summary>
        /// UnDealMemo
        /// <summary>
        public string UnDealMemo { get; set; }
        /// <summary>
        /// ModuleID
        /// <summary>
        public Guid ModuleID { get; set; }
        /// <summary>
        /// Files
        /// <summary>
        public string Files { get; set; }
        /// <summary>
        /// ConfirmDate
        /// <summary>
        public DateTime? ConfirmDate { get; set; }
        /// <summary>
        /// ConfirmMan
        /// <summary>
        public string ConfirmMan { get; set; }
        /// <summary>
        /// SignMan
        /// <summary>
        public string SignMan { get; set; }
        /// <summary>
        /// NotHandleDate
        /// <summary>
        public DateTime? NotHandleDate { get; set; }
        /// <summary>
        /// NotHandleMan
        /// <summary>
        public string NotHandleMan { get; set; }
        /// <summary>
        /// SignManDate
        /// <summary>
        public DateTime? SignManDate { get; set; }
        /// <summary>
        /// ConfirmStatus
        /// <summary>
        public string ConfirmStatus { get; set; }
        /// <summary>
        /// UnConfirmDate
        /// <summary>
        public DateTime? UnConfirmDate { get; set; }
        /// <summary>
        /// UnConfirmMan
        /// <summary>
        public string UnConfirmMan { get; set; }
        /// <summary>
        /// UnSignManDate
        /// <summary>
        public DateTime? UnSignManDate { get; set; }
        /// <summary>
        /// UnSignMan
        /// <summary>
        public string UnSignMan { get; set; }
        /// <summary>
        /// DemandFinishMan
        /// <summary>
        public string DemandFinishMan { get; set; }
        /// <summary>
        /// DemandFinishDate
        /// <summary>
        public DateTime? DemandFinishDate { get; set; }
        /// <summary>
        /// JudgeMan
        /// <summary>
        public string JudgeMan { get; set; }
        /// <summary>
        /// JudgeDate
        /// <summary>
        public DateTime? JudgeDate { get; set; }
        /// <summary>
        /// AdditionalPoints
        /// <summary>
        public int? AdditionalPoints { get; set; }
        /// <summary>
        /// DealLaterMan
        /// <summary>
        public string DealLaterMan { get; set; }
        /// <summary>
        /// DealLaterDate
        /// <summary>
        public DateTime? DealLaterDate { get; set; }
        /// <summary>
        /// DealLaterReason
        /// <summary>
        public string DealLaterReason { get; set; }
        /// <summary>
        /// FilesDate
        /// <summary>
        public DateTime? FilesDate { get; set; }
        /// <summary>
        /// OverdueReasons
        /// <summary>
        public string OverdueReasons { get; set; }
        /// <summary>
        /// ConfirmMan2
        /// <summary>
        public string ConfirmMan2 { get; set; }
        /// <summary>
        /// ConfirmDate2
        /// <summary>
        public DateTime? ConfirmDate2 { get; set; }
    }

    //public partial class Demand
    //{
    //    /// <summary>
    //    /// 主键
    //    /// </summary>
    //    public Guid ID { get; set; }
    //    /// <summary>
    //    /// 单据编号
    //    /// </summary>
    //    public string BillCode { get; set; }
    //    /// <summary>
    //    /// 客户id
    //    /// </summary>
    //    public Guid CustomerID { get; set; }
    //    /// <summary>
    //    /// 录入日期
    //    /// </summary>
    //    public DateTime RecordDate { get; set; }
    //    /// <summary>
    //    /// 提出者
    //    /// </summary>
    //    public string Presenter { get; set; }
    //    /// <summary>
    //    /// 制单人
    //    /// </summary>
    //    public string Maker { get; set; }
    //    /// <summary>
    //    /// 制单日期
    //    /// </summary>
    //    public DateTime MakeDate { get; set; }

    //    /// <summary>
    //    /// 需求描述
    //    /// </summary>
    //    public string Describe { get; set; }

    //    /// <summary>
    //    /// 审核人
    //    /// </summary>
    //    public string Audit { get; set; }
    //    /// <summary>
    //    /// 审核时间
    //    /// </summary>
    //    public DateTime AuditDate { get; set; }
    //    /// <summary>
    //    /// 拒批人
    //    /// </summary>
    //    public string Rejector { get; set; }
    //    /// <summary>
    //    /// 拒批时间 
    //    /// </summary>
    //    public DateTime RejectDate { get; set; }

    //}
}
