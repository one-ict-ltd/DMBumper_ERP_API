namespace ONEERP.Areas.Inventory.Models
{
    public class PromoPacketingDetailsVM
    {
        public int PacketingDetailId { get; set; }
        public int? packetingMasterId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? requisitionDetailId { get; set; }
        public int? requisitionQty { get; set; }
        public int? transferQty { get; set; }
    }
    public class PromoBulkPacketingDetailsVM
    {
        public string locationCode { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public int? productSubCategoryId { get; set; }
        public int productWiseSpecificationId { get; set; }
        public int? promoRequisitionDetailsId { get; set; }
        public int? transferQty { get; set; }
    }
}
