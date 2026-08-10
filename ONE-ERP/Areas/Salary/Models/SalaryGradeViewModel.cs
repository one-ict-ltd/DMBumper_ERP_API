namespace ONEERP.Areas.Salary.Models
{
    public class SalaryGradeViewModel
    {
        public int salaryGradeId { get; set; }
        public string gradeName { get; set; }
        public string payScale { get; set; }
        public decimal? basicAmount { get; set; }
        public decimal? currentBasic { get; set; }
        public int? sortOrder { get; set; }
        public bool? isActive { get; set; }
    }
}
