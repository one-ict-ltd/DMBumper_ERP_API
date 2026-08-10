using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnApprovalLog : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int approvalLogId { get; set; }
        public int? approvalTypeId { get; set; }
        public CmnApprovalType approvalType { get; set; }
        public int? approverTypeId { get; set; }
        public CmnApproverType approverType { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? masterId { get; set; }
        public int? currentApproverId { get; set; }
        public int? nextApproverId { get; set; }
        public string description { get; set; }
        public string comments { get; set; }

        public int? productTypeId { get; set; }
        public int? status { get; set; }
    }
}
