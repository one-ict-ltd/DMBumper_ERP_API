using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryHead : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryHeadId { get; set; }
        [MaxLength(200)]
        public string salaryHeadName { get; set; }
        [MaxLength(50)]
        public string headShortName { get; set; }
        [MaxLength(100)]
        public string salaryHeadCode { get; set; }
        [MaxLength(10)]
        public string salaryHeadType { get; set; }
        public int? sortOrder { get; set; }       
        public bool? isIncomeTax { get; set; }       
        public bool? isInvestments { get; set; }       
        public bool? isAdvance { get; set; }       
        public bool? isArrear { get; set; }        
        public bool? isBonus { get; set; }       
        public bool? isMonthlyAllowance { get; set; }  
        public bool? isLoan { get; set; }
    }
}
