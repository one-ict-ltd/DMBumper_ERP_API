using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalaryInvestmentRebateSettings : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int investmentRebateSettingsId { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }
        public decimal? allowableInvestment { get; set; }
        public decimal? investmentRebate { get; set; }
        public decimal? orInvestmentRebate { get; set; }
        
    }
}
