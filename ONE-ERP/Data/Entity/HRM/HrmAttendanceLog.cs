using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendanceLog : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int logId { get; set; } 
        public int? employeeid { get; set; } // obsolete
        public int punchCardNo { get; set; }// this is User_ID from devices
        public DateTime? punchDate { get; set; }
        public int? verifyState { get; set; }
        public int? verifyType { get; set; }
        public int? workCode { get; set; }
        public DateTime? insertedDate { get; set; }       
        public int? machineNumber { get; set; }
    }
}
