using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoStockReceiveMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoStockRecieveMasterId { get; set; }
        [MaxLength(50)]
        public string promoStockReceiveNo { get; set; } 
        public DateTime? promoStockReceiveDate { get; set; }
        public int? promoStockTypeId { get; set; }
        public InvStockType promoStockType { get; set; } 
        public string depotCode { get; set; }
        public int? transactionMasterId { get; set; }   
        public string purpose { get; set; }

        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
