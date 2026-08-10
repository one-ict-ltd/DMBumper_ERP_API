using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendanceShiftGroupDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int shiftDetailId { get; set; }
        public int? shiftMasterId { get; set; }
        public HrmAttendanceShiftGroupMaster shiftMaster { get; set; }
        [MaxLength(20)]
        public string weekDay { get; set; }
        [MaxLength(20)]
        public string startTime { get; set; }
        [MaxLength(20)]
        public string endTime { get; set; }
        public bool? isHoliday { get; set; }        
        
    }
}
