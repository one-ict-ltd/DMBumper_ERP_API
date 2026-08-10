using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseOrderViewModel
    {
        public int? purchaseOrderId { get; set; }
        public int? purchaseReqId { get; set; }
        public DateTime? purchaseOrderDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public int? supplierId { get; set; }
        public int? toWarehouseId { get; set; }
        public int? purchaseOrderFromId { get; set; }
        public string approvalStatus { get; set; }
        public string purpose { get; set; }
        public bool? isUrgency { get; set; }
        public bool? isActive { get; set; }
        public string lcNo { get; set; }       
        public string refNo { get; set; }
        public int? transactionTypeId { get; set; }

        public int? purchaseFromId { get; set; }
        public int? csMasterId { get; set; }
        public int? requisitionFinalizeMasterId { get; set; }
        public List<PurchaseOrderDetailsViewModel> lstPurOrderDetailsViewModel { get; set; }
        public List<POWiseTermsAndConditionsViewModel> poWiseTermsAndConditions { get; set; }

       
        public decimal? grossAmount { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? totalDiscount { get; set; }
        public decimal? freightCharge { get; set; }
        public decimal? netAmount { get; set; }
        public bool? isAutoStock { get; set; }
        public int? purchaseOrderSignatoryId { get; set; }
        // public List<PurchaseDetailsViewModel> lstPurchaseDetailsViewModels { get; set; }
    }
}
