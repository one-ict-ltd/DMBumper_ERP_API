using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpOffshoreCharge:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpOffshoreChargeId { get; set; }
        public int? ImpLCInfoMasterId { get; set; }
        public PurImpLCInfoMaster ImpLCInfoMaster { get; set; }
        public decimal? OffshoreBankCharge { get; set; }
        public DateTime? OffshoreBankChargeDate { get; set; }
        public string remarks { get; set; }
    }
}
