using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductionIssueDetailViewModel
    {
        public int? productIssueDetailId { get; set; }
        public int? requisitinDetailId { get; set; }
        public decimal? qty { get; set; }       
    }
}
