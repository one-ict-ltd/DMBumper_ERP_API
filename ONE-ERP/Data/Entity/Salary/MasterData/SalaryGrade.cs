using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryGrade: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryGradeId { get; set; }
        [MaxLength(100)]
        public string gradeName { get; set; }
        [MaxLength(100)]
        public string payScale { get; set; }
        public decimal? basicAmount { get; set; }       
        public decimal? currentBasic { get; set; }
        public int? sortOrder { get; set; }
    }
    public class SalaryDepot: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryDepotId { get; set; }
        [MaxLength(20)]
        public string salaryDepotCode { get; set; }
        [MaxLength(100)]
        public string salaryDepotName { get; set; }
        public int? sortOrder { get; set; }
    }
}
