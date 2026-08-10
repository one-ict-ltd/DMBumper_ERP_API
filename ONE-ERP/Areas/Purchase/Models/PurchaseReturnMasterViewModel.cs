using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseReturnMasterViewModel
    {
        public int purchaseReturnMasterId { get; set; }
        public int? purchaseOrderId { get; set; }
        public int? partyId { get; set; }
        public int? storeId { get; set; }
        public string purchaseReturnNo { get; set; }
        public DateTime? purchaseReturnDate { get; set; }
        public decimal? grossAmount { get; set; }
        public decimal? totalVatAmount { get; set; }
        public decimal? totalAitAmount { get; set; }
        public decimal? freightChargeAmount { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? netAmount { get; set; }
        public string comments { get; set; }
        public bool? isClose { get; set; }
        public List<PurchaseReturnDetailsViewModel> lstDetailsViewModel { get; set; }

    }
}
