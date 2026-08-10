using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeExperienceViewModel
    {
        public int? employeeExperienceId { get; set; }
        public int? employeeId { get; set; }
        public string organization { get; set; }
        public string appointedDesignation { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string remarks { get; set; }
    }
}
