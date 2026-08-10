using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoRequisitionUploadDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoRequisitionDetailsId { get; set; }
        public int? promoRequisitionMasterId { get; set; }
        public PromoRequisitionUploadMaster promoRequisitionMaster { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public string areaManagerCode { get; set; }
        public string regionCode { get; set; }
        public string productCode { get; set; }
        public decimal? quantity { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? isPacketDistributeToDepot { get; set; }

    }
}
