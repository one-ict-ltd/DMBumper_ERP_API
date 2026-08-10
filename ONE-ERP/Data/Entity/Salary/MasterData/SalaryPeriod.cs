using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryPeriod : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryPeriodId { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }  
        public int? salaryTypeId { get; set; }
        public SalaryType salaryType { get; set; }
        public int? bonusTypeId { get; set; }
        public SalaryBonusType bonusType { get; set; }        
        [MaxLength(200)]
        public string periodName { get; set; }
        [MaxLength(10)]
        public string monthName { get; set; }
        public int? lockStatus { get; set; }
        [MaxLength(100)]
        public string lockBy { get; set; }
        public DateTime? lockDate { get; set; }
        public decimal? workingDays { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
    }
}
