using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalProductSpecWiseIncentivePolicy : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int incentivePolicyId { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int productWiseSpecificationId { get; set; }
        public decimal minOrderQty { get; set; }
        public string uom { get; set; }
        public string incentiveType { get; set; }
        public decimal incentiveValue { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? toDate { get; set; } 
        public int? collUpToDays { get; set; }
        [DefaultValue(1)]
        public int hasGeneralBonus { get; set; }
    }
}
