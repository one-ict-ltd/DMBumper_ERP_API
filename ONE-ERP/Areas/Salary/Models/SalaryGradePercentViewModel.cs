namespace ONEERP.Areas.Salary.Models
{
    public class SalaryGradePercentViewModel
    {
        public int salaryGradePercentId { get; set; }
        public int? salaryGradeId { get; set; }
        public int? salaryHeadId { get; set; }
        public int? salaryCalulationTypeId { get; set; }
        public decimal? percentAmount { get; set; }
        public bool? isActive { get; set; }
    }
}
