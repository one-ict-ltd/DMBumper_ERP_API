using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class BankInsuranceChargeDetailsViewModel
    {
        public int ImpChargeHeadId { get; set; }
        public int ChargeDetailsId { get; set; }
        public decimal amount { get; set; }
        public int paticularId { get; set; }
        
    }
}
