using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class DepotPromoDistributionMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoDistributionMasterId { get; set; }
        public DateTime? promoDistributionDate { get; set; }
        public string promoDistributionNo { get; set; }
        public int? fromSbuId { get; set; }
        public CmnSpecialBranchUnit fromSbu { get; set; } 
        public int? promoReceiveMasterId { get; set; } 
        public DepotPromoReceiveMaster promoReceiveMaster { get; set; }
        public string Purpose { get; set; }
        public int? isReceived { get; set; } 
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
