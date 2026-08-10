using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLoanEntry : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int loadId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? LoanCategoryId { get; set; }
        public HrmLoanCategory LoanCategory { get; set; }
        public int? interestTypeId { get; set; }
        public HrmLoanInterestType interestType { get; set; }

        public string applicationNo { get; set; }
        public DateTime? applicationDate { get; set; }
        public DateTime? issueDate { get; set; }
        public string registrationNo { get; set; }
        public string engineNo { get; set; }
        public decimal? interestRate { get; set; }
        public int? NumOfInstallment { get; set; }
        public decimal? AmountOfInstallment { get; set; }
        public decimal? loanAmount { get; set; }
        public int? salaryCalulationTypeId { get; set; }
        public SalaryCalulationType salaryCalulationType { get; set; }

        public decimal? purchaseAmount { get; set; }
        public DateTime? purchaseDate { get; set; }
        public bool? isClose { get; set; }
    }
}
