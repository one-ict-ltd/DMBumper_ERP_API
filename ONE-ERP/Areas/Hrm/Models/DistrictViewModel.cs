using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class DistrictViewModel
    {
        public int? districtsId { get; set; }
        public string districtCode { get; set; }
        public string districtName { get; set; }
        public string shortName { get; set; }
        public int divisionsId { get; set; }
        public bool? isActive { get; set; }
    }
}
