using System;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductWisePricingViewModel
    {
        public int pricingId { get; set; }
        public DateTime effectiveDate { get; set; }
        public decimal? price { get; set; }
        public int? productId { get; set; }
        public int? colorId { get; set; }     
        public int? sizeId { get; set; }
        public string barCode { get; set; }
        //public bool? isActive { get; set; }
        //public bool? isDelete { get; set; }

    }
}
