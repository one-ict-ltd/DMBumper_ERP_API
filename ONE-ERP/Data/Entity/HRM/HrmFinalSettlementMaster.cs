using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Data.Entity.HRM
{
    public class HrmFinalSettlementMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int finalSettlementMasterId { get; set; }
        public int employeeId { get; set; }
        public decimal grossSalary { get; set; }
        public decimal basicSalary { get; set; }
        public decimal lastMonthSalary { get; set; }
        public string lMSalaryStatus { get; set; }
        public string mcInstallmentNo { get; set; }

        public decimal mcInstallmentAmmount { get; set; }
        public string employmentType { get; set; }
        public string pFEligibility { get; set; }
        public DateTime? resignationDate { get; set; }
        public string pFContributionDuration { get; set; }
        public string noticeProvided { get; set; }
        public decimal pFAmount { get; set; }
        public DateTime? LWD { get; set; }
        public string aLBalance { get; set; }
        public string noticeShortfall { get; set; }
        public string lengthOfService { get; set; }
        public DateTime? resignationEffectiveDate { get; set; }
        public string serviceBenefitDuration { get; set; }
        public int? isApprove { get; set; }
    }
}
