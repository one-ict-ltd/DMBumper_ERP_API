using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseDetailsViewModel
    {
        public int? purchaseOrderDetailsId { get; set; }
        public int? purchaseOrderId { get; set; }        
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? avgPurchasePrice { get; set; }
        public decimal? price { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? costPrice { get; set; }
        public decimal? totalAmount { get; set; }
        public bool? isAutoStock { get; set; }
    }
}
