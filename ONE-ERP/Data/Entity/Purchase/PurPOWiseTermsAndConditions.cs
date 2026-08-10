using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPOWiseTermsAndConditions : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int poWiseTermsAndConditionsId { get; set; }
        public int? purchaseOrderId { get; set; }
        public PurPurchaseOrder purchaseOrder { get; set; }
        public int? supplierId { get; set; }
        public AccParty party {get;set;}
        public string termsAndConditions { get; set; }

        public int? termsAndConditionId { get; set; }
        public PurTermsAndConditions termsAndCondition { get; set; }
        
    }
}
