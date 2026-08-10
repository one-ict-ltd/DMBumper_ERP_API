using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseOrderDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int purchaseOrderDetailsId { get; set; }
        public int? purchaseOrderId { get; set; }
        public PurPurchaseOrder purchaseOrder { get; set; }
        public int? purchaseReqDetailsId { get; set; }
        public PurPurchaseReqDetails purchaseReqDetails { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
      

        public int? csDetailId { get; set; }
        public PurCSDetail csDetail { get; set; }
        
        public int? requisitionFinalizeDetailId { get; set; }
        public PurRequisitionFinalizeDetail requisitionFinalizeDetail { get; set; }

        public decimal? reqQty { get; set; }
        public decimal? avgPurchasePrice { get; set; }
        public decimal? price { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? costPrice { get; set; }
        public decimal? totalAmount { get; set; }

        public decimal? vatAmount { get; set; }
        public decimal? discountAmount { get; set; }
        public int? BudgetCreateId { get; set; }
    }
}
