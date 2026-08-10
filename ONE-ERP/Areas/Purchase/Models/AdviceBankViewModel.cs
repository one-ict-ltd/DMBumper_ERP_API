using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class AdviceBankViewModel
    {
        public int adviceBankId { get; set; }
        public string adviceBankName { get; set; }
        public string adviceBankCode { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public bool isActive { get; set; }
    }
}
