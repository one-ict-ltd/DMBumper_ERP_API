using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class BenificiaryViewModel
    {
        public int benificiaryId { get; set; }
        public string benificiaryName { get; set; }
        public string benificiaryCode { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public bool isActive { get; set; }
    }
}
