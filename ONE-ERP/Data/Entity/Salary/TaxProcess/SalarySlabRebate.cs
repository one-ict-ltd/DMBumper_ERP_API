using ONEERP.Data.Entity.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalarySlabRebate : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int slabRebateId { get; set; }
        public int? rebateSlabTypeId { get; set; }
        public SalaryRebateSlabType rebateSlabType { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }
        [MaxLength(300)]
        public string slabRebateText { get; set; }
        public decimal? slabRebateAmount { get; set; }
        public decimal? taxRate { get; set; }       
        public int? sortOrder { get; set; }
        
    }
}
