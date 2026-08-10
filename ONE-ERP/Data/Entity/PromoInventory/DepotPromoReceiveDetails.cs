using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class DepotPromoReceiveDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoReceiveDetailsId { get; set; }
        public string territoryCode { get; set; }
        public string areaCode { get; set; }
        public string regionCode { get; set; }
        public int? transferQuantity { get; set; }
        public int? depotPromoReceiveMasterId { get; set; }
        public DepotPromoReceiveMaster depotPromoReceiveMaster { get; set; }  
    }
}
