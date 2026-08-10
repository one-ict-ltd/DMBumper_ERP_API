using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockOutByBarcode
    {
        public int stockMasterId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? storeId { get; set; }
        public string stockNo { get; set; }
        public DateTime? stockDate { get; set; }
        public int? stockTypeId { get; set; }
        public int? stockCategoryId { get; set; }
        public string remarks { get; set; }
        public bool? isActive { get; set; }
        public int? transactionMasterId { get; set; }
        public List<StockOutByBarcodeDetails> lstStockOutByBarcodeDetails { get; set; }
    }
}
