using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ChargeHeadViewModel
    {
        public int chargeHeadId { get; set; }
        public string chargeHeadName { get; set; }
        public string shortName { get; set; }
        public string shortOrder { get; set; }
        public string chargeCode { get; set; }
        public bool isActive { get; set; }

    }
}
