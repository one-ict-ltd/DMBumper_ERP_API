using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeTypeViewModel
    {
        public int? employeeTypeId { get; set; }
        public string empType { get; set; }
        public string empTypeBn { get; set; }
        public string shortName { get; set; }
        public bool? isActive { get; set; }
    }
}
