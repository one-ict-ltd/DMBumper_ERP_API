using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvStockReceiveDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stockReceiveDetailsId { get; set; }
        public int? stockReceiveId { get; set; }
        public InvStockReceive stockReceive { get; set; }
        public int? productTrnfrDetailsId { get; set; }
        public InvProductTransferDetails productTransferDetails { get;set;}
        public int? storeId { get; set; }
        public CmnStore store { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? stockReceiveQty { get; set; }
        public decimal? CntQty { get; set; }
        public decimal? looseQty { get; set; }
        public int? toUomId { get; set; }
        public decimal? price { get; set; }
        public string batchNo { get; set; }
    }
}
