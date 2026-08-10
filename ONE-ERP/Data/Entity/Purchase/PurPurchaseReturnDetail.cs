using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseReturnDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int purchaseReturnDetailId { get; set; }
        public int? purchaseReturnMasterId { get; set; }
        public PurPurchaseReturnMaster purchaseReturnMaster { get; set; }
        public int? purchaseOrderDetailsId { get; set; }
        public PurPurchaseOrderDetails purchaseOrderDetails { get; set; }

        public decimal? returnQty { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? totalAmount { get; set; }
    }
}
