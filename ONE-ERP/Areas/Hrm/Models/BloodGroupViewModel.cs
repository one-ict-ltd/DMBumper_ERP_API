using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class BloodGroupViewModel
    {
        public int? bloodGroupId { get; set; }
        public string Name { get; set; }
        public string shortName { get; set; }
        public bool? isActive { get; set; }
    }
}
