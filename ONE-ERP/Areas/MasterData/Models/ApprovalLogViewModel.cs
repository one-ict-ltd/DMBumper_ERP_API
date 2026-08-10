using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class ApprovalLogViewModel
    {
        public int approvalLogId { get; set; }
        public int? approvalTypeId { get; set; }
        public int? approverTypeId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? masterId { get; set; }
        public int? currentApproverId { get; set; }
        public int? nextApproverId { get; set; }
        public string description { get; set; }
        public bool? isActive { get; set; }
    }
}
