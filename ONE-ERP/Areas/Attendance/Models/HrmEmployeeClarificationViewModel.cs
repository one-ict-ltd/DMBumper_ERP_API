using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Models
{
    public class HrmEmployeeClarificationViewModel
    {
        public int employeecClarificationId { get; set; }
        public int empId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string clarification { get; set; }
        public int approvalStatus { get; set; }
    }
}
