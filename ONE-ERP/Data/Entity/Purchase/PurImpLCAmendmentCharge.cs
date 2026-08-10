using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpLCAmendmentCharge:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpLCAmendmentChargeId { get; set; }

        public int? ImpLCAmendmentId { get; set; }
        public PurImpLCAmendment ImpLCAmendment { get; set; }
        public DateTime? AmendmentChargeDate { get; set; }
        public decimal? AmendmentAmount { get; set; }
        public string remarks { get; set; }
    }
}
