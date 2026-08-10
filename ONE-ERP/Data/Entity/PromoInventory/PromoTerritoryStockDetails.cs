using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoTerritoryStockDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int territoryStockDetailId { get; set; } 
        public int? territoryStockMasterId { get; set; }
        public PromoTerritoryStockMaster territoryStockMaster { get; set; } 
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }

        public int? packetingDetailId { get; set; }
        public PromoPacketingDetails packetingDetail  { get; set; }

        public decimal? stockQty { get; set; }    
      
    }
}
