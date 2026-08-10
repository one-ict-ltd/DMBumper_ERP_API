using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalarySlab : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salarySlabId { get; set; }
        public int? salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }
        [MaxLength(100)]
        public string slabName { get; set; }        
        public decimal? slabAmount { get; set; }
        public DateTime? effectiveDate { get; set; }
        public int? sortOrder { get; set; }

    }
}
