using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class GRNQAViewModel
    {
        public int? approvalStatus { get; set; }
        public string InitialOrRetest { get; set; }
        public List<grnlist> grnModel { get; set; }
        public DateTime? RetestDate { get; set; }
}
    public class grnlist
    {
        public int? grnMasterId { get; set; }
        public int? grnDetailsId { get; set; }
        public int? grnStatus { get; set; }
        public bool? isSelect { get; set; }
        public decimal? potency { get; set; }
        public decimal? approvedQty { get; set; }
        public string QCRefNo { get; set; }
        public int?  grnLogMasterId { get; set; }
    }
}
