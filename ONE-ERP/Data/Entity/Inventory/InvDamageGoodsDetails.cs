using ONEERP.Data.Entity.Purchase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvDamageGoodsDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int damageGoodsDetailsId { get; set; }
        public int? damageGoodsId { get; set; }
        public InvDamageGoods damageGoods { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? damageQty { get; set; }
        public int? stockTypeId { get; set; }
        public InvStockType stockType { get; set; }
        public string remarks { get; set; }
        public int? barcodeDetailsId { get; set; }
        public InvStockInWithBarcodeDetails barcodeDetails { get; set; }


    }
}
