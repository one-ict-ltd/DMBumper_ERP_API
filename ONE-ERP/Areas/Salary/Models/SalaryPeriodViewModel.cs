using System;

namespace ONEERP.Areas.Salary.Models
{
    public class SalaryPeriodViewModel
    {
        public int salaryPeriodId { get; set; }
        public int? fiscalYearId { get; set; }
        public int? salaryTypeId { get; set; }
        public int? bonusTypeId { get; set; }
        public string periodName { get; set; }
        public string monthName { get; set; }
        public int? lockStatus { get; set; }  //SEE CmnDropDown TABLE FOR DETAILS
        public string lockBy { get; set; }
        public DateTime? lockDate { get; set; }
        public decimal? workingDays { get; set; }
        public bool? isActive { get; set; }
    }
}
