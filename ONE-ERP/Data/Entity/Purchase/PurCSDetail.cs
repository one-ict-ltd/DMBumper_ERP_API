using ONEERP.Data.Entity.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurCSDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int csDetailId { get; set; }

        public int? CSMasterId { get; set; }
        public PurCSMaster CSMaster { get; set; }

        public int? PartyId { get; set; }
        public AccParty Party { get; set; }


        public int? quotationCollectionDetailId { get; set; }
        public PurQuotationCollectionDetail quotationCollectionDetail { get; set; }

        public decimal? qty { get; set; }

        public decimal? rate { get; set; }

        public decimal? vatAmount { get; set; }

        public int? rateFrom { get; set; }  // 1- SIGHT Rate - Cash   2-Deferred Rate -Credit 
        public string manufactureOrigin { get; set; }
        public int? BudgetCreateId { get; set; }
        public decimal? Discount { get; set; }
    }
}
