using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class DivisionViewModel
    {
        public int? divisionsId { get; set; }
        public string divisionCode { get; set; }
        public string divisionName { get; set; }
        public string shortName { get; set; }
        public int countryId { get; set; }
        public bool? isActive { get; set; }
    }
}
