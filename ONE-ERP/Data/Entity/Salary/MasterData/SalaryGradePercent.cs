using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryGradePercent : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryGradePercentId { get; set; }
        public int? salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }
        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }
        public int? salaryCalulationTypeId { get; set; }
        public SalaryCalulationType salaryCalulationType { get; set; }
        public decimal? percentAmount { get; set; }

    }
}
