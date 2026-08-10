using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductSetDetailsViewModel
    {
        public int productSetDetailsId { get; set; }
        public int productSetMasterId { get; set; }
        public int accessories_ProductWiseSpecificationId { get; set; }
        public int qty { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }
        public bool? isSelect { get; set; }
    }
}
