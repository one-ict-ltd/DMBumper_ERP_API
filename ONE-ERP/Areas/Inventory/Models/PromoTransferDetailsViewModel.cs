namespace ONEERP.Areas.Inventory.Models
{
    public class PromoTransferDetailsViewModel
    {
        public int? packetingMasterId { get; set; }
        public int packetDistributionDetailsId { get; set; }
        public string territoryCode { get; set; }
        public int? transferQuantity { get; set; }
    }
}
