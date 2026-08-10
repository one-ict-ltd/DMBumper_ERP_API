using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeaveApprovalLog:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveApprovalLogId { get; set; }

        public int? HrmLeaveRegisterId { get; set; }
        public HrmLeaveRegister HrmLeaveRegister { get; set; }

        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }

        public DateTime? date { get; set; }

        public int? isApprove { get; set; }
        public int? seqNo { get; set; }
        public int? status { get; set; } //0 = Applied | 1 = ongoing | 2 = approved | 3 = rejected

        public string comment { get; set; }
        public string description { get; set; }
    }
}
