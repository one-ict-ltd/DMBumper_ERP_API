using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class FundSourceViewModel
    {
        public int? fundSourceId { get; set; }
        public string fundSourceName { get; set; }
        public string aliasName { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }

    }
}
