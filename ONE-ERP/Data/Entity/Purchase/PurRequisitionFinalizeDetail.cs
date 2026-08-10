using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurRequisitionFinalizeDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requisitionFinalizeDetailId { get; set; }

        public int? PurRequisitionFinalizeMasterId { get; set; }
        public PurRequisitionFinalizeMaster PurRequisitionFinalizeMaster { get; set; }

        public int? PurchaseReqDetailsId { get; set; }
        public PurPurchaseReqDetails PurchaseReqDetails { get; set; }

        public int? isCS { get; set; }

        public int? PartyId { get; set; }
        public AccParty Party { get; set; }

        public decimal? qty { get; set; }

        public decimal? rate { get; set; }

        public decimal? vatPercentage { get; set; }
        public decimal? vatAmount { get; set; }

        public int? BudgetCreateId { get; set; }
    }
}
