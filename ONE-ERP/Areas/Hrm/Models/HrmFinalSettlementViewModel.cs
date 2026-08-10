using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class HrmFinalSettlementViewModel
    {
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
        public List<HrmFinalSettlementDetailViewModel> finalSettlementDetails { get; set; }
        public List<HrmFinalSettlementSignatoryViewModel> SignatoryList { get; set; }
    }
    public class HrmFinalSettlementDetailViewModel
    {
        public int finalSettlementDetailsId { get; set; }
        public int finalSettlementMasterId { get; set; }
        public int finalSettlementHeadId { get; set; }
        public string monthOrParticulars { get; set; }
        public string days { get; set; }
        public decimal amount { get; set; }


    }
    public class HrmFinalSettlementSignatoryViewModel
    {
        public int signatoryId { get; set; }
        public string signatoryType { get; set; }
        public int sortOrder { get; set; }
        public int finalSettlementHeadId { get; set; }
        public int employeeId { get; set; }
        public int status { get; set; }
        public int finalSettlementMasterId { get; set; }
        public string remarks { get; set; }
        public int? isApprove { get; set; }

    }
}
