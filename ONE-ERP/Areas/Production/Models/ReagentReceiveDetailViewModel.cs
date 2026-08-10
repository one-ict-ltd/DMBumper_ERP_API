using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ReagentReceiveDetailViewModel
    {
        public int? reagentReceiveDetailId { get; set; }
        public int? reagentIssueDetailId { get; set; }
        public decimal? qty { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
    }
}
