using Internal.Common.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{
    //public partial class ClientFile
    //{
    //    public Customer Customer { get; set; }
    //}
    /// <summary>
    /// ClientFile 
    /// </summary>
    public partial class ClientFile:BaseModel<Guid>
    {
        //[SugarColumn(IsPrimaryKey = true)]
        //public override Guid ID { get => base.ID; set => base.ID = value; }
        /// <summary>
        /// RowNo
        /// </summary>
        public int RowNo { get; set; }
        /// <summary>
        /// AutoID
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// BillType
        /// </summary>
        public string BillType { get; set; }
        /// <summary>
        /// IsStop
        /// </summary>
        public bool? IsStop { get; set; }
        /// <summary>
        /// Mender
        /// </summary>
        public string Mender { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ClientCode
        /// </summary>
        public string ClientCode { get; set; }
        /// <summary>
        /// ClientName
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// ClientAbbrName
        /// </summary>
        public string ClientAbbrName { get; set; }
        /// <summary>
        /// Area
        /// </summary>
        public Guid Area { get; set; }
        /// <summary>
        /// Industry
        /// </summary>
        public Guid Industry { get; set; }
        /// <summary>
        /// BelongTo
        /// </summary>
        public Guid BelongTo { get; set; }
        /// <summary>
        /// ClientCategory
        /// </summary>
        public Guid ClientCategory { get; set; }
        /// <summary>
        /// Grade
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// ClientSource
        /// </summary>
        public string ClientSource { get; set; }
        /// <summary>
        /// SaleArea
        /// </summary>
        public Guid SaleArea { get; set; }
        /// <summary>
        /// Credibility
        /// </summary>
        public string Credibility { get; set; }
        /// <summary>
        /// ClientState
        /// </summary>
        public string ClientState { get; set; }
        /// <summary>
        /// MainContactor
        /// </summary>
        public string MainContactor { get; set; }
        /// <summary>
        /// Telephone
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Department
        /// </summary>
        public Guid Department { get; set; }
        /// <summary>
        /// Saler
        /// </summary>
        public string Saler { get; set; }
        /// <summary>
        /// Fax
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// PostCode
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// Documentary
        /// </summary>
        public string Documentary { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// OfficePhone
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// CloseUser
        /// </summary>
        public string CloseUser { get; set; }
        /// <summary>
        /// CloseDate
        /// </summary>
        public DateTime? CloseDate { get; set; }
        /// <summary>
        /// Website
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// SoftwareUse
        /// </summary>
        public string SoftwareUse { get; set; }
        /// <summary>
        /// SuitedProduct
        /// </summary>
        public string SuitedProduct { get; set; }
        /// <summary>
        /// MachineNumber
        /// </summary>
        public int? MachineNumber { get; set; }
        /// <summary>
        /// Scale
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// DropDown
        /// </summary>
        public string DropDown { get; set; }
        /// <summary>
        /// Reference
        /// </summary>
        public Guid Reference { get; set; }
        /// <summary>
        /// ClientNameID
        /// </summary>
        public Guid ClientNameID { get; set; }
        /// <summary>
        /// ClientRelationship
        /// </summary>
        public string ClientRelationship { get; set; }
        /// <summary>
        /// RegisteredCapital
        /// </summary>
        public string RegisteredCapital { get; set; }
        /// <summary>
        /// EmployeesNumber
        /// </summary>
        public string EmployeesNumber { get; set; }
        /// <summary>
        /// AnnualSalesVolume
        /// </summary>
        public string AnnualSalesVolume { get; set; }
        /// <summary>
        /// LegalPerson
        /// </summary>
        public string LegalPerson { get; set; }
        /// <summary>
        /// EstablishmentDate
        /// </summary>
        public string EstablishmentDate { get; set; }
        /// <summary>
        /// StopDate
        /// </summary>
        public DateTime? StopDate { get; set; }
        /// <summary>
        /// StopOper
        /// </summary>
        public string StopOper { get; set; }
        /// <summary>
        /// Formal
        /// </summary>
        public bool? Formal { get; set; }
        /// <summary>
        /// Maker
        /// </summary>
        public string Maker { get; set; }
        /// <summary>
        /// MakeDate
        /// </summary>
        public DateTime? MakeDate { get; set; }
        /// <summary>
        /// ServiceEndDate
        /// </summary>
        public DateTime? ServiceEndDate { get; set; }
        /// <summary>
        /// FollowType
        /// </summary>
        public string FollowType { get; set; }
        /// <summary>
        /// ContractNO
        /// </summary>
        public string ContractNO { get; set; }
        /// <summary>
        /// ReceivingReport
        /// </summary>
        public string ReceivingReport { get; set; }
        /// <summary>
        /// TransferDate
        /// </summary>
        public DateTime? TransferDate { get; set; }
        /// <summary>
        /// ImplementBeginDate
        /// </summary>
        public DateTime? ImplementBeginDate { get; set; }
        /// <summary>
        /// BuyingMemo
        /// </summary>
        public string BuyingMemo { get; set; }
        /// <summary>
        /// UseBegin
        /// </summary>
        public DateTime? UseBegin { get; set; }
        /// <summary>
        /// UseEnd
        /// </summary>
        public DateTime? UseEnd { get; set; }
        /// <summary>
        /// ServerEnd
        /// </summary>
        public DateTime? ServerEnd { get; set; }
        /// <summary>
        /// InstallDate
        /// </summary>
        public DateTime? InstallDate { get; set; }
        /// <summary>
        /// InspectDate
        /// </summary>
        public DateTime? InspectDate { get; set; }
        /// <summary>
        /// ReturnVisitDays
        /// </summary>
        public int? ReturnVisitDays { get; set; }
        /// <summary>
        /// BugCount
        /// </summary>
        public int? BugCount { get; set; }
        /// <summary>
        /// ConfirmCount
        /// </summary>
        public int? ConfirmCount { get; set; }
    }
}
