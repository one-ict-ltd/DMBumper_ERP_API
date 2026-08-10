using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseOrderReceiveDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int poReceiveDetailsId { get; set; }
        public int? poReceiveId { get; set; }
        public PurPurchaseOrderReceive purPurchaseOrderReceive { get; set; }
        public int? purchaseOrderDetailsId { get; set; }
        public PurPurchaseOrderDetails purchaseOrderDetails { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? price { get; set; }
        public decimal? receiveQty { get; set; }
        [MaxLength(100)]
        public string receiveStatus { get; set; }
    }
}
