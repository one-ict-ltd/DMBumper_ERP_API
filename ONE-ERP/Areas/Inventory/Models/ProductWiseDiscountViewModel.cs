using System;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductWiseDiscountViewModel
    {
        public int? discountId { get; set; }
        public int? discountTypeId { get; set; }
        public int? productId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        //public bool? Active { get; set; }
       // public int? productWiseSizeId { get; set; }  
        public decimal? discountAmountOrPercentage { get; set; }
       // public bool? isActive { get; set; }
        //public bool? isDelete { get; set; }     
        public bool? isAmount { get; set; }
    }
}
