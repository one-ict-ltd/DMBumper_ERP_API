using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductCategorySpecificationViewModel
    {
        public int? productCategorySpecificationId { get; set; }
        public int? productCategoryId { get; set; }
        public string specificationType { get; set; }        
        public bool? isActive { get; set; }



    }
}
