using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class RequisitionFinalMasterDetailViewmodel
    {
        public int requisitionFinalizeDetailId { get; set; }

        public int? PurRequisitionfinalizeMasterId { get; set; }

        public int? PurchaseReqDetailsId { get; set; }

        public int? isCS { get; set; }

        public int? PartyId { get; set; }

        public decimal? finalQty { get; set; }

        public decimal? rate { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? vatPercentage { get; set; }

        public int? BudgetCreateId { get; set; }
        public string prodSpecification { get; set; }


    }
}
