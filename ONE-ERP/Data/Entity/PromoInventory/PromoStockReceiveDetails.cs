using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoStockReceiveDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoStockReceiveDetailsId { get; set; }
        public int? promoStockReceiveMasterId { get; set; }
        public PromoStockReceiveMaster promoStockReceiveMaster { get; set; } 
        public decimal? stockReceiveQty { get; set; }  
    }
}
