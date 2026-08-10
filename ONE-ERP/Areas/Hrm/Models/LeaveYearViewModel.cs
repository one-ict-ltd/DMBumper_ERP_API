using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class LeaveYearViewModel
    {
        public int leaveYearId { get; set; }
        public string yearName { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }
    }
}
