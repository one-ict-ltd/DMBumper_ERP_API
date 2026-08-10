using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpChargeHead:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpChargeHeadId { get; set; }
        public string chargeHeadName { get; set; }
        public string chargeHeadCode { get; set; }
        public string chargeHeadShortName { get; set; }
        public int? sortOrder { get; set; }
    }
}
