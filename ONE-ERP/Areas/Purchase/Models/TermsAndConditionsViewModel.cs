using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class TermsAndConditionsViewModel
    {
        public int? termsAndCoditionsId { get; set; }
        public int? productTypeId { get; set; }
        public int? supplierId { get; set; }
        public string termsAndConditions { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }
    }
}
