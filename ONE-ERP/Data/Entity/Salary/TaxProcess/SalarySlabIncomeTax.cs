using ONEERP.Data.Entity.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalarySlabIncomeTax : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int slabIncomeTaxId { get; set; }
        public int? slabTypeId { get; set; }
        public SalarySlabType slabType { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }
        public decimal? slabAmount { get; set; }
        public decimal? taxRate { get; set; }       
        public int? sortOrder { get; set; }
        [MaxLength(200)]
        public string slabText { get; set; }
    }
}
