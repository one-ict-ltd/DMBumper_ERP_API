namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseReturnDetailsViewModel
    {   
        public int purchaseReturnDetailId { get; set; }
        public int? purchaseReturnMasterId { get; set; }
        public int? purchaseOrderDetailsId { get; set; }
        public decimal? returnQty { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? totalAmount { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }

    }
}
