namespace ONEERP.Areas.Salary.Models
{
    public class SalaryHeadViewModel
    {
        public int salaryHeadId { get; set; }
        public string salaryHeadName { get; set; }
        public string headShortName { get; set; }
        public string salaryHeadCode { get; set; }
        public string salaryHeadType { get; set; }
        public int? sortOrder { get; set; }
        public bool? isIncomeTax { get; set; }
        public bool? isInvestments { get; set; }
        public bool? isAdvance { get; set; }
        public bool? isArrear { get; set; }
        public bool? isBonus { get; set; }
        public bool? isMonthlyAllowance { get; set; }
        public bool? isLoan { get; set; }
        public bool? isActive { get; set; }
    }
}
