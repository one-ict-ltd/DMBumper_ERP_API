namespace ONEERP.Areas.Inventory.Models
{
    public class PromoPacketNoDetailsVM
    {
        public int PacketNoDetailId { get; set; }
        public int? packetingMasterId { get; set; }
        public string packetNo { get; set; }
        public string refNo { get; set; }
    }
}
