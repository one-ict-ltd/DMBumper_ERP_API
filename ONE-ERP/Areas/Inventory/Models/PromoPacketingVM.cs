using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class PromoPacketingVM
    {
        public int packetingMasterId { get; set; }
        public DateTime? packetingMasterDate { get; set; }
        public string packetingMasterNo { get; set; }
        public int? promoRequisitionId { get; set; }
        public string territoryCode { get; set; }
        public int? totalPacket { get; set; }
        public string packetNames { get; set; }
        public string refNo { get; set; }
        public string remarks { get; set; }
        public string packetingFor { get; set; }

        public List<PromoPacketingDetailsVM> lstDetailsViewModel { get; set; }
        public List<PromoPacketNoDetailsVM> lstPacketDetailsViewModel { get; set; }

    }
    public class PromoBulkPacketingVM
    {
        public int packetingMasterId { get; set; }
        public DateTime? packetingMasterDate { get; set; }
        public string packetingMasterNo { get; set; }
        public int promoRequisitionId { get; set; }
        public string territoryCode { get; set; }
        public int? totalPacket { get; set; }
        public string packetNames { get; set; }
        public string refNo { get; set; }
        public string remarks { get; set; }
        public string packetingFor { get; set; }

        public List<PromoBulkPacketingDetailsVM> allPacketListModel { get; set; }

    }
}
