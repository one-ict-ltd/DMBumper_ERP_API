using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesCategoryWiseProductDetailsVM
    {
        public int? productId { get; set; }
        public string productName { get; set; }
        public bool? isChecked { get; set; }
        public int? salesCategoryWiseProductMasterId { get; set; }
        public int? salesCategoryWiseProductDetailsId { get; set; }
    }
}
