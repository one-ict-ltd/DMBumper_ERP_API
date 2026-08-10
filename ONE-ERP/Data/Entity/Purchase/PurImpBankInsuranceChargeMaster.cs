using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpBankInsuranceChargeMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpBankInsuranceChargeMasterId { get; set; }

        public int? ImpLCInfoMasterId { get; set; }
        public PurImpLCInfoMaster ImpLCInfoMaster { get; set; }

        public int? insuranceCompanyId { get; set; }
        public PurInsuranceCompany insuranceCompany { get; set; }

        public string Type { get; set; }

        public DateTime? BankChargeDate { get; set; }
        public DateTime? InsuranceDate { get; set; }

        public string InsuranceNo { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceBranch { get; set; }
        public decimal? InsuranceAmount { get; set; }

        public string DocumnetNo { get; set; }
        public string remarks { get; set; }
    }
}
