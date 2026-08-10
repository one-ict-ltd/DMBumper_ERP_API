using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class DesignationViewModel
    {
        public int? designationId { get; set; }
        public string designationCode { get; set; }
        public string designationName { get; set; }
        public string shortName { get; set; }
        public bool? isActive { get; set; }
    }
}
