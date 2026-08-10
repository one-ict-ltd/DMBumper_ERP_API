using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class LeaveOpeningBalanceViewModel
    {
        public int leaveOpeningBalanceId { get; set; }
        public int? employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public int? leaveTypeId { get; set; }
        public string leaveTypeName { get; set; }
        public int? yearId { get; set; }
        public string yearName { get; set; }
        public int? leaveDays { get; set; }
        public int? leaveCarryDays { get; set; }
        public bool? isActive { get; set; }
    }

}
