using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpBenificiary:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpBenificiaryId { get; set; }
        public string benificiaryName { get; set; }
        public string benificiaryCode { get; set; }
        public string benificiaryShortName { get; set; }
        public int? sortOrder { get; set; }
    }
}
