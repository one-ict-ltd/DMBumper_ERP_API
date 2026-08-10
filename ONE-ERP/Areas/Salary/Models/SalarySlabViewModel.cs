using System;

namespace ONEERP.Areas.Salary.Models
{
    public class SalarySlabViewModel
    {
        public int salarySlabId { get; set; }
        public int? salaryGradeId { get; set; }
        public string slabName { get; set; }
        public decimal? slabAmount { get; set; }
        public DateTime? effectiveDate { get; set; }
        public bool? isActive { get; set; }
    }
}
