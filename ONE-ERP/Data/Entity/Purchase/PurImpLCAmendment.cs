using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpLCAmendment:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpLCAmendmentId { get; set; }

        public int? ImpLCInfoMasterId { get; set; }
        public PurImpLCInfoMaster ImpLCInfoMaster { get; set; }

        public string AmendmentNo { get; set; }
        public string AmendmentCause { get; set; }
        public DateTime? AmendmentDate { get; set; }
        public string remarks { get; set; }
    }
}
