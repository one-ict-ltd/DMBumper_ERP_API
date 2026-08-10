using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class AmendmentViewModel
    {
        public int ImpLCAmendmentId { get; set; }
        public int ImpLCInfoMasterId { get; set; }
        public string amendmentNo { get; set; }
        public DateTime amendmentDate { get; set; }
        public string amendment { get; set; }
        public string remarks { get; set; }

    }
}
