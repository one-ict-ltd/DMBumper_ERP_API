using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttandanceClarification : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int attandanceClarificationId { get; set; }
        public int?  employeeId { get; set; }
        public DateTime? attandanceClarificationDate { get; set; }
        public string attandanceClarificationTime { get; set; }
        public string narration { get; set; }
        public int? attandanceClarificationTypeId { get; set; }
        public bool? isApproved { get; set; }
    }
}
