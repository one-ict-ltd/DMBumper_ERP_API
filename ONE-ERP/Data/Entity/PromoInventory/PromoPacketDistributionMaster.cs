using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoPacketDistributionMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int packetDistributionId { get; set; }
        public string packetDistributionNo { get; set; } 
        public DateTime? packetDistributionDate { get; set; }
        public int? fromSbuId { get; set; }
        public CmnSpecialBranchUnit fromSbu { get; set; }
        public int? toSbuId { get; set; }
        public CmnSpecialBranchUnit toSbu { get; set; }
        public int? fromStoreId { get; set; }
        public CmnStore fromStore { get; set; }
        public string Purpose { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }

    }
}
