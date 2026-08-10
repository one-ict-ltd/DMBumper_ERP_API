using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    //Requisition
    public class PrdRmPmMiscellaneousReq : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RmPmMiscReqId { get; set; }
        public string RmPmMiscReqNo { get; set; }
        public DateTime? RmPmMiscReqDate { get; set; }
        public int? productTypeId { get; set; }
        public int? miscReqTypeId { get; set; }
        public string reqFrom { get; set; }
        public string reqPurpose { get; set; }
        public string gatePassNo { get; set; }
        public DateTime? gatePassDate { get; set; }
        public bool? hasIssued { get; set; }
    }
    public class PrdRmPmMiscellaneousReqDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RmPmMiscReqDetailId { get; set; }
        public int? RmPmMiscReqId { get; set; }
        public PrdRmPmMiscellaneousReq RmPmMiscReq { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public string remarks { get; set; }
    }

    //Issue
    public class PrdRmPmMiscellaneousIssue : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RmPmMiscIssueId { get; set; }
        public string RmPmMiscIssueNo { get; set; }
        public DateTime? RmPmMiscIssueDate { get; set; }
        public int? RmPmMiscReqId { get; set; }
        public PrdRmPmMiscellaneousReq RmPmMiscReq { get; set; }
        public string issuePurpose { get; set; }
        public string gatePassNo { get; set; }
        public DateTime? gatePassDate { get; set; }
    }
    public class PrdRmPmMiscellaneousIssueDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RmPmMiscIssueDetailId { get; set; }
        public int? RmPmMiscIssueId { get; set; }
        public PrdRmPmMiscellaneousIssue RmPmMiscIssue { get; set; }
        public int? RmPmMiscReqDetailId { get; set; }
        public PrdRmPmMiscellaneousReqDetails RmPmMiscReqDetail { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? IssueQty { get; set; }
        public string batchNo { get; set; }
        public string remarks { get; set; }
    }
}
