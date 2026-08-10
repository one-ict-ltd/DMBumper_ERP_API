using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurGrnLogViewModel
    {
        public int grnLogMasterId { get; set; }
        public int grnDetailsId { get; set; }
        public DateTime? RetestDate { get; set; }
        public decimal? TestReqQty { get; set; }
        public int? NoOfPackForRetest { get; set; }
        public DateTime? prevRetestDate { get; set; }
        public string grnType { get; set; }

    }
}
