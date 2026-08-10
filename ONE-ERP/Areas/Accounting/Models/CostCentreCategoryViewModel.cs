using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostCentreCategoryViewModel
    {
        public int? costCentreCategoryId { get; set; }

        public string costCentreCategoryName { get; set; }

        public string costCentreCategoryCode { get; set; }

        public bool? isActive { get; set; }
    }
}
