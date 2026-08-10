using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    //public class RmPmReqIssueViewModel
    //{
    //}
    public class PrdRmPmMiscellaneousReqViewModel
    {
        public int RmPmMiscReqId { get; set; }
        public string RmPmMiscReqNo { get; set; }
        public DateTime? RmPmMiscReqDate { get; set; }
        public int? productTypeId { get; set; }
        public int? miscReqTypeId { get; set; }
        public string reqFrom { get; set; }
        public string reqPurpose { get; set; }
        public string gatePassNo { get; set; }
        public DateTime? gatePassDate { get; set; }
        public List<PrdRmPmMiscellaneousReqDetailsViewModel> lstDetail { get; set; }
    }
    public class PrdRmPmMiscellaneousReqDetailsViewModel
    {
        public int RmPmMiscReqDetailId { get; set; }
        public int? RmPmMiscReqId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public string remarks { get; set; }
    }

    //Issue
    public class PrdRmPmMiscellaneousIssueViewModel
    {
        public int RmPmMiscIssueId { get; set; }
        public string RmPmMiscIssueNo { get; set; }
        public DateTime? RmPmMiscIssueDate { get; set; }
        public int? RmPmMiscReqId { get; set; }
        public string issuePurpose { get; set; }
        public string gatePassNo { get; set; }
        public DateTime? gatePassDate { get; set; }
        public List<PrdRmPmMiscellaneousIssueDetailsViewModel> lstDetail { get; set; }
    }
    public class PrdRmPmMiscellaneousIssueDetailsViewModel
    {
        public int RmPmMiscIssueDetailId { get; set; }
        public int? RmPmMiscIssueId { get; set; }
        public int? RmPmMiscReqDetailId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? IssueQty { get; set; }
        public string batchNo { get; set; }
        public string remarks { get; set; }
    }
}
