using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductionReceiveDetailViewModel
    {
        public int? productReceiveDetailId { get; set; }
        public int? issueDetailId { get; set; }
        public decimal? qty { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
    }
}
