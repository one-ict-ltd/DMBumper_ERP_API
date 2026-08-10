using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockOutByBarcodeDetails
    {
        public int? stockDetailsId { get; set; }
        public int stockMasterId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? stockQty { get; set; }
        public int? transactionDetailsId { get; set; }
        public int? stockTypeId { get; set; }
        public int? poReceiveId { get; set; }
        public int? poReceiveDetailsId { get; set; }
        public int? poQty { get; set; }
        public decimal? purchaseRate { get; set; }
    }
}
