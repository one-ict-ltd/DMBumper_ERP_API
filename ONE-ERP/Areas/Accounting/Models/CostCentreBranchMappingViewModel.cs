using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostCentreBranchMappingViewModel
    {
        public int? costCentreMappingId { get; set; }
        public int? costCentreId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }

    }
}
