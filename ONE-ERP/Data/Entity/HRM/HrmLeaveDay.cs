using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeaveDay:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveDayId { get; set; }
        public string leaveDayName { get; set; }
        public string leaveDayNameBn { get; set; }
        public string description { get; set; }
        public DateTime? dayStartTime { get; set; }
        public DateTime? dayEndTime { get; set; }
    }
}
