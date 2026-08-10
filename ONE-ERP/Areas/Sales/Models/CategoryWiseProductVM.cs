using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class CategoryWiseProductVM
    {
        public int? month { get; set; }
        public string year { get; set; }
        public int? productCategoryId { get; set; }
        public List<SalesCategoryWiseProductDetailsVM> lstDetailsViewModel { get; set; }
    }
}
