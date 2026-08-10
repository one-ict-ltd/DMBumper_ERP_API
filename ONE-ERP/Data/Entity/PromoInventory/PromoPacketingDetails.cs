using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoPacketingDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PacketingDetailId { get; set; } 
        public int? packetingMasterId { get; set; }
        public PromoPacketingMaster packetingMaster { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; } 
        public int? requisitionDetailId { get; set; }
        public PromoRequisitionUploadDetails requisitionDetail { get; set; }
        public int? requisitionQty  { get; set; }
        public int? transferQty  { get; set; }
      
    }
}
