using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class MunicipilityLocationViewModel
    {
        public int? MunicipilityLocationId { get; set; }
        public string locationName { get; set; }
        public string shortName { get; set; }
        public bool? isActive { get; set; }
    }
}
