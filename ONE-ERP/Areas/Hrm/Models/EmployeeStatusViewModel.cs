using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeStatusViewModel
    {
        public int? employeeStatusId { get; set; }
        public string statusName { get; set; }
        public string statusShortName { get; set; }
        public bool isActive { get; set; }
    }
}
