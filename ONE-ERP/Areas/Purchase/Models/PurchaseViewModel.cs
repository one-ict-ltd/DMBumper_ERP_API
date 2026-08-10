using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseViewModel
    {
        public int? purchaseOrderId { get; set; }
        public DateTime? purchaseOrderDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public int? supplierId { get; set; }
        public string purpose { get; set; }
        public decimal? grossAmount { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? totalDiscount { get; set; }
        public decimal? freightCharge { get; set; }
        public decimal? netAmount { get; set; }
        public bool? isAutoStock { get; set; }
        public string lcNo { get; set; }
        public string refNo { get; set; }
        public int? transactionTypeId { get; set; }
        public List<PurchaseDetailsViewModel> lstPurchaseDetailsViewModels { get; set; }
        public List<POWiseTermsAndConditionsViewModel> poWiseTermsAndConditions { get; set; }

    }
}
