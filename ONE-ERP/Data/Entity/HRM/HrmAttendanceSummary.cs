using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendanceSummary : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int summaryId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }    
        public DateTime? attendanceMonth { get; set; }
        public int? weeklyOff { get; set; }
        public int? holiday { get; set; }
        public int? leave { get; set; }
        public int? present { get; set; }
        public int? absent { get; set; }
        public int? late { get; set; }
        public int? earlyIn { get; set; }
        public int? earlyOut { get; set; }
        public int? consider { get; set; }
        public int? totalDays { get; set; }
        public bool? isApproved { get; set; }
        public string remarks { get; set; }
    }
}
