using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurQuotationCollectionDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int quotationCollectionDetailId { get; set; }

        public int? PurQuotationCollectionMasterId { get; set; }
        public PurQuotationCollectionMaster PurQuotationCollectionMaster { get; set; }

        public int? PartyId { get; set; }
        public AccParty Party { get; set; }

        public decimal? qty { get; set; }

        public decimal? rate { get; set; }  // SIGHT Rate - Cash
        public decimal? deferredRate  { get; set; } //  Deferred Rate -Credit 
        public string manufactureOrigin { get; set; }
        public int? PurRequisitionFinalizeDetailId { get; set; }
        public int? productWiseSpecificationId { get; set; }

        public decimal? VatPercent { get; set; }

        public decimal? VatAmount { get; set; }  
        public decimal? TotalRate { get; set; }
        public int? BudgetCreateId { get; set; }

        public decimal? Discount { get; set; }

    }
}
