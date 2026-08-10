using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoDistributionDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoDistributionDetailsId { get; set; }
        public PromoDistributionMaster promoDistributionMaster { get; set; }
    }
}
