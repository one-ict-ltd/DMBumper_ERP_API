using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalaryTaxChallan : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taxChallanId { get; set; }
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        [MaxLength(50)]
        public string taxChallanNo { get; set; }
        public DateTime? challanDate { get; set; }
        
    }
}
