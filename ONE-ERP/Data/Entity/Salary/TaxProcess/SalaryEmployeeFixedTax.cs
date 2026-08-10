using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.HRM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalaryEmployeeFixedTax : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeFixedTaxId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }
        public decimal? taxAmount { get; set; }
        public int? noOfPeriod { get; set; }      
    }
}
