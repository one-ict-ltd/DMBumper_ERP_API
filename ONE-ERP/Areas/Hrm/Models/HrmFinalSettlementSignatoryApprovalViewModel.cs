using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class HrmFinalSettlementSignatoryApprovalViewModel
    {
        public int approvalStatus { get; set; }
        public List<HrmSignatoryViewModel> finalSettlementApprovalModel { get; set; }
    }
    public class HrmSignatoryViewModel
    {
        public int signatoryId { get; set; }
        public int finalSettlementMasterId { get; set; }
        public int status { get; set; }
        public string remarks { get; set; }
        public int? isApprove { get; set; }
        public bool? isSelect { get; set; }

    }
}
