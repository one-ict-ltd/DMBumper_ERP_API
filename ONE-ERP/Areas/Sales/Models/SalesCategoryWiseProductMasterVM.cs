using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesCategoryWiseProductMasterVM
    {
        public int salesCategoryWiseProductMasterId { get; set; }
        public int monthId { get; set; }
        public string year { get; set; }
        public int salCategorySalesId { get; set; }
    }
}
