using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeaveOpeningBalance:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveOpeningBalanceId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? leaveTypeId { get; set; }
        public HrmLeaveType leaveType { get; set; }
        public int? yearId { get; set; }
        public HrmLeaveYear year { get; set; }
        public int? leaveDays { get; set; }
        public int? leaveCarryDays { get; set; }
    }
}
