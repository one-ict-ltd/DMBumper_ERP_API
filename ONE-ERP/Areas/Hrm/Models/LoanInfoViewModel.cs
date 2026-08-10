using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class LoanInfoViewModel
    {
        public int loadId { get; set; }
        public int? employeeId { get; set; }
        public int? LoanCategoryId { get; set; }
        public int? interestTypeId { get; set; }

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

        public decimal? purchaseAmount { get; set; }
        public DateTime? purchaseDate { get; set; }
        public bool? isClose { get; set; }
    }
}
