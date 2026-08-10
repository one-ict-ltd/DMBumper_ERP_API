using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendancePunchCard : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int punchCardId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? shiftMasterId { get; set; }
        public HrmAttendanceShiftGroupMaster shiftMaster { get; set; }
        [MaxLength(40)]
        public string punchCardNo { get; set; }
        
    }
}
