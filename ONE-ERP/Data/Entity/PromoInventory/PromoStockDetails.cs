using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoStockDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoStockDetailsId { get; set; }
        public int? promoStockMasterId { get; set; }
        public PromoStockMaster promoStockMaster { get; set; }
        public string territoryCode { get; set; }
        public string areaCode { get; set; }
        public string regionCode { get; set; }
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
