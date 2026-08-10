using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoDistributionMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoDistributionId { get; set; }
        public DateTime? promoDistributionDate { get; set; }
        public InvStockType promoStockType { get; set; }

        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
