using ONEERP.Data.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvStockDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stockDetailsId { get; set; }
        public int? stockMasterId { get; set; }
        public InvStockMaster stockMaster { get; set; }
        public int? poReceiveId { get; set; }
        public PurPurchaseOrderReceive purchaseOrderReceive { get; set; }
        public int? poReceiveDetailsId { get; set; }
        public int? productId { get; set; }
        public InvProduct product{get;set;}
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? stockTypeId { get; set; }
        public InvStockType stockType { get; set; }
        public int? transactionDetailsId { get; set; }
        public decimal? poQty { get; set; }
        public decimal? stockQty { get; set; }
        public decimal? purchaseRate { get; set; }
        public decimal? CntQty { get; set; }
        public decimal? looseQty { get; set; }
        public int? toUomId { get; set; }
        public decimal? currentRate { get; set; }


        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }
    }
}
