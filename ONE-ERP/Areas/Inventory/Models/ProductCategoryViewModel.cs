using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductCategoryViewModel
    { 
        public int? productCategoryId { get; set; }
        public string categoryName { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }
        public List<ProductCategorySpecificationViewModel> lstDetail { get; set; }


    }
}
