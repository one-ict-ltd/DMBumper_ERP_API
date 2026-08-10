using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeaveApprovalMatrix : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveApprovalMatrixId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }

        public int? approverId { get; set; }
        public HrmEmployee approver { get; set; }

        public int? seqNo { get; set; }

        public bool? isFinalApproval { get; set; }
    }
}
