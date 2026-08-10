using ONEERP.Data.Entity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class DepotPromoDistributionDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int distributionDetailsId { get; set; }
        public string territoryCode { get; set; }
        public string areaCode { get; set; }
        public string regionCode { get; set; }
        public int? transferQuantity { get; set; }
        public int? promoDistributionMasterId { get; set; }
        public DepotPromoDistributionMaster promoDistributionMaster { get; set; }
        public int? isTerritoryReceived { get; set; }
    }
}
