using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class ApprovalMatrixViewModel
    {
        public int approvalMatrixId { get; set; }
        public int? approvalTypeId { get; set; }
        public int? approverTypeId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? nextApproverId { get; set; }
        public int? sequenceNo { get; set; }
        public string matrixName { get; set; }
        public bool? isActive { get; set; }
        public List<ApprovalMatrixViewModel> lstDetails { get; set; }
    }
}
