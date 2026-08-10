using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoPacketingMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int packetingMasterId { get; set; }
        public DateTime? packetingMasterDate { get; set; }
        public string packetingMasterNo { get; set; }
        public int? promoRequisitionId { get; set; }
        public PromoRequisitionUploadMaster promoRequisition  { get; set; } 
        public string territoryCode { get; set; } 
        public string areaCode { get; set; }
        public string regionCode { get; set; }
        public int? totalPacket  { get; set; }
        public string packetNames { get; set; }
        public string  refNo { get; set; }
        public string  remarks { get; set; }

        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public string packetingFor { get; set; }
    }
}
