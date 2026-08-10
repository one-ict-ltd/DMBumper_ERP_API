using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockMasterViewModel
    {
        public int stockMasterId { get; set; }
        public int? poReceiveId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? storeId { get; set; }
        public string stockNo { get; set; }
        public DateTime stockDate { get; set; }
        public string stockTypeId { get; set; }
        public int? stockStatusId { get; set; }
        public int? transactionMasterId { get; set; }
        public string remarks { get; set; }
        public bool? isActive { get; set; }
        public string purchaseOrderNo { get; set; }
        public string challanNo { get; set; }
        public string lcNo { get; set; }
        public string supplierName { get; set; }

        public List<StockDetailsViewModel> stockDetailsList { get; set; }

    }
}
