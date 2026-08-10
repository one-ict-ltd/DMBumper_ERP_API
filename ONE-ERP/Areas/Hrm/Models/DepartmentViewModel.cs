using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class DepartmentViewModel
    {
        public int? departmentId { get; set; }
        public string deptCode { get; set; }
        public string deptName { get; set; }
        public string shortName { get; set; }
        public DateTime startDate { get; set; }
        public bool? isActive { get; set; }
    }
}
