
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class OffshoreChargeViewModel
    {
        public int ImpOffshoreChargeId { get; set; }
        public int ImpLCInfoMasterId { get; set; }
        public int OffshoreBankCharge { get; set; }
        public DateTime OffshoreBankChargeDate { get; set; }
        public string remarks { get; set; }
        
    }
}
