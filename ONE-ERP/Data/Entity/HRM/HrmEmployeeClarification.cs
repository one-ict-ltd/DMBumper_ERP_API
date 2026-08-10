using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HrmMaster;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeClarification : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeecClarificationId { get; set; }
        public int employeeId { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public string clarification { get; set; }
        public int approvalStatus { get; set; }
    }
}
