using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalaryIncomeTaxSetup : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int incomeTaxSetupId { get; set; }
        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }
        [MaxLength(500)]
        public string exemptionRule { get; set; }
        [MaxLength(100)]
        public string exemption { get; set; }
        public decimal? exemptionAmount { get; set; }
        public decimal? exemptionPercent { get; set; }
        public decimal? exemptionMaxAmount { get; set; }        
        public int? sortOrder { get; set; }
        
    }
}
