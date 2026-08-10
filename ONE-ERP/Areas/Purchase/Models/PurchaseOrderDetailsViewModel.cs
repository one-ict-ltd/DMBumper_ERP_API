using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseOrderDetailsViewModel
    {
        public int? purchaseOrderDetailsId { get; set; }
        public int? purchaseOrderId { get; set; }
        public int? purchaseReqDetailsId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? price { get; set; }

        public int? csDetailId { get; set; }
        public int? requisitionFinalizeDetailId { get; set; }
        public bool? isActive { get; set; }

        public decimal? avgPurchasePrice { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? costPrice { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? amount { get; set; }
        public bool? isAutoStock { get; set; }
        public int? BudgetCreateId { get; set; }
        public string prodSpecification { get; set; }
    }
}
