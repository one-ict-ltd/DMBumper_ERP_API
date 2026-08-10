using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostCentreViewModel
    {
        public int? costCentreId { get; set; }
        public string costCentreName { get; set; }
        public string aliasName { get; set; }
        public int? departmentId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }

        public int? AccCostCenterCategoryId { get; set; }
        public int? AccCostCenterLocationId { get; set; }

    }
}
