using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpBankInsuranceChargeDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpBankInsuranceChargeDetailId { get; set; }

        public int? ImpBankInsuranceChargeMasterId { get; set; }
        public PurImpBankInsuranceChargeMaster ImpBankInsuranceChargeMaster { get; set; }

        public int? ImpChargeHeadId { get; set; }
        public PurImpChargeHead ImpChargeHead { get; set; }

        public decimal? Amount { get; set; }
    }
}
