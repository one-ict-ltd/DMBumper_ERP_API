using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostCentreLocationViewModel
    {
        public int? costCentreLocationId { get; set; }

        public string costCentreLocationName { get; set; }

        public string costCentreLocationCode { get; set; }

        public bool? isActive { get; set; }
    }
}
