using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Models
{
    public class PunchCardViewModel
    {
        public int punchCardId { get; set; }
        public int? employeeId { get; set; }
        public int? shiftMasterId { get; set; }
        public string punchCardNo { get; set; }
        public bool? isActive { get; set; }

        public string callName { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public string department { get; set; }

    }
}
