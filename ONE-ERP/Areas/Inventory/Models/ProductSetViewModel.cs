using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductSetMasterViewModel
    {
        public int productSetMasterId { get; set; }
        public int companyId { get; set; }
        public int sbuId { get; set; }
        public int master_ProductWiseSpecificationId { get; set; }
        public string ProductSetName { get; set; }
        public bool isActive { get; set; }
        public bool isDelete { get; set; }
        public List<ProductSetDetailsViewModel> lstDetails { get; set; }
    }
}
