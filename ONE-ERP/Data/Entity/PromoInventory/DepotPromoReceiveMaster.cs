using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class DepotPromoReceiveMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int depotPromoReceiveId { get; set; }
        public DateTime? depotPromoReceiveDate { get; set; }
        public string promoReceivedNo { get; set; }
        public string Purpose { get; set; }
        public int? packetDistributionId { get; set; }
        public PromoPacketDistributionMaster packetDistribution  { get; set; }
        public int? receivedSbuId { get; set; }
        public CmnSpecialBranchUnit receivedSbu { get; set; }
        public int? isDistribute { get; set; }

        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
