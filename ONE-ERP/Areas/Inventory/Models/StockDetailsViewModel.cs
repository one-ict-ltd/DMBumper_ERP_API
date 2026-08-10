using System;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockDetailsViewModel
    {
        public int stockDetailsId { get; set; }
        public int? stockMasterId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? stockQty { get; set; }
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }

        public int? poReceiveId { get; set; }
        public int? poReceiveDetailsId { get; set; }
        public int? FGStockDetailId { get; set; }
        public decimal? poQty { get; set; }
        public decimal? purchaseRate { get; set; }
        public bool? isActive { get; set; }

        public decimal? currentRate { get; set; }

    }
}
