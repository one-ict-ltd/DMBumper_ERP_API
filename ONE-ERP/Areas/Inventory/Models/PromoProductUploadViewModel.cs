using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class PromoProductUploadViewModel
    {
        public string skuNumber { get; set; }
        public string skuName { get; set; }
        //public string packSize { get; set; }
        public string productCategory { get; set; }
        public string brand { get; set; }
    }
}
