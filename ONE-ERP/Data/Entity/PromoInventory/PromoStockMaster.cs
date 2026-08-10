using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoStockMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoStockMasterId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public DateTime? promoStockDate { get; set; }
        public string promoStockNo { get; set; }
        public int? promoStockTypeId { get; set; }
        public InvStockType promoStockType { get; set; }
        public int? storeId { get; set; }
        public CmnStore store { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? stockCategoryId { get; set; }
        public InvStockCategory stockCategory { get; set; }
        public string remarks { get; set; }
        public int? transactionMasterId { get; set; }


    }
}
