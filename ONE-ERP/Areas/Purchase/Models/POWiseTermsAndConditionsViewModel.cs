using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class POWiseTermsAndConditionsViewModel
    {
        public int? poWiseTermsAndConditionsId { get; set; }
        public int? purchaseOrderId { get; set; }
        public int? termsAndConditionId { get; set; }
        public string termsAndConditions { get; set; }
        public bool? isActive { get; set; }
        public bool? Active { get; set; }
        public bool? isDelete { get; set; }
        public int? supplierId { get; set; }
    }
}
