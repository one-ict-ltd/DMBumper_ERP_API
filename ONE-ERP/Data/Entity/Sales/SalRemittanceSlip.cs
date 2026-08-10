using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalRemittanceSlip : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int remittanceSlipId { get; set; }

        public string resourceUrl { get; set; }

        public int? remittanceId { get; set; }
        public SalRemittance remittance { get; set; }
        public int? remittanceMasterId { get; set; }
        public SalRemittanceMaster remittanceMaster { get; set; }
    }

    public class SalRemittanceAdjustment : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [MaxLength(20)]
        public string fromDepot { get; set; }
        [MaxLength(20)]
        public string toDepot { get; set; }
        public DateTime? date { get; set; }
        public decimal? amount { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? remittanceMasterId { get; set; }
        public SalRemittanceMaster remittanceMaster { get; set; }
    }
}