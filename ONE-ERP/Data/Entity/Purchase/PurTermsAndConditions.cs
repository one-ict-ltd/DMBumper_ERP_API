using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurTermsAndConditions:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int termsAndCoditionsId { get; set; }
        public int? supplierId { get; set; }
        public AccParty party { get; set; }

        public int? productTypeId { get; set; }
        public InvProductType productType { get; set; }
        public string termsAndConditions { get; set; }
    }
}
