using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ComparativeStatementDetailViewModel
    {
        public int? CsDetailId { get; set; }
        public int? csMasterId { get; set; }
        public decimal? approvedqty { get; set; }
        public bool? isSelect { get; set; }
        public int? partyId { get; set; }
        public string partyName { get; set; }
        public string comments { get; set; }
        public decimal? qty { get; set; }
        public decimal? rate { get; set; }
        public decimal? total { get; set; }
        public int? rateFrom { get; set; }
        public int? quotationCollectionDetailId { get; set; }
        public string manufactureOrigin { get; set; }
        public int? BudgetCreateId { get; set; }
        public decimal? Discount { get; set; }
    }
}
