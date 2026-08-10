using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class ActivityTypeViewModel
    {
        public int? activityTypeId { get; set; }
        public string activityTypeName { get; set; }
        public bool isActive { get; set; }
    }
}
