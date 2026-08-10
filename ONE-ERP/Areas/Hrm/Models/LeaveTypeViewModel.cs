using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class LeaveTypeViewModel
    {
        public int leaveTypeId { get; set; }
        public string typeName { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }
    }
}
