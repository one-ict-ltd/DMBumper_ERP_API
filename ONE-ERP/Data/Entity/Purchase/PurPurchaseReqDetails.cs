using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseReqDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int purchaseReqDetailsId { get; set; }
        public int? purchaseReqId { get; set; }
        public PurPurchaseRequisition purchaseRequisition { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? productReqDetailsId { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? price { get; set; }

        //public int? purchaseOrderDetailId { get; set; }
        //public PurPurchaseOrderDetails purchaseOrderDetail { get; set; }
        public decimal? receivedQty { get; set; }
        public decimal? currentStockQty { get; set; }
        public decimal? vatAmount { get; set; }
        public string prodSpecification { get; set; }
        public int? purchaseOrderDetailId { get; set; }

        public int? revisionId { get; set; }
        public PurRequisitionRevision purRequisitionRevision { get; set; }
    }
}
