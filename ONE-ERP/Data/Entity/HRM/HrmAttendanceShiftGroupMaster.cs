using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendanceShiftGroupMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int shiftMasterId { get; set; }        
        [MaxLength(300)]
        public string shiftName { get; set; }
        
    }
}
