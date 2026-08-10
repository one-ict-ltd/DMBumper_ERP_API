using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ComparativeStatementMasterViewModel
    {
        public int? csMasterId { get; set; }
        public int? quotationCollectionMasterId { get; set; }

        public string csMasterNo { get; set; }

        public DateTime? csDate { get; set; }

        public string productName { get; set; }

        public string remarks { get; set; }

        public int? ApprovalStatus { get; set; }

        public List<ComparativeStatementDetailViewModel> lstCSDetailsViewModel { get; set; }
    }
}
