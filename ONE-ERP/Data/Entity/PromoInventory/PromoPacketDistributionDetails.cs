using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoPacketDistributionDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int packetDistributionDetailsId { get; set; }
        public string territoryCode { get; set; }
        public string areaCode { get; set; }
        public string regionCode { get; set; }
        public int? transferQuantity { get; set; }
        public int? promoPacketDistributionMasterId { get; set; }
        public PromoPacketDistributionMaster promoPacketDistributionMaster { get; set; }
        public int? packetingMasterId { get; set; }
        public PromoPacketingMaster packetingMaster { get; set; }
    }
}
