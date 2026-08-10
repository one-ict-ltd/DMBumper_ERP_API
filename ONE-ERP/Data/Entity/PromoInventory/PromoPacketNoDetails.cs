using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoPacketNoDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PacketNoDetailId { get; set; } 
        public int? packetingMasterId { get; set; }
        public PromoPacketingMaster packetingMaster { get; set; } 
        public string packetNo { get; set; }  
        public string refNo { get; set; }
    }
}
