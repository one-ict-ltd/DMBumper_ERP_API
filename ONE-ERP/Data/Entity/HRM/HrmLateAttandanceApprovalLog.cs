using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLateAttandanceApprovalLog : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int lateAttandanceApprovalLog { get; set; }

        public int? attandanceClarificationId { get; set; }
        public HrmAttandanceClarification attandanceClarification { get; set; }

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
