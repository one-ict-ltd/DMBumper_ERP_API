using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryCalulationType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryCalulationTypeId { get; set; }
        [MaxLength(100)]
        public string salaryCalulationTypeName { get; set; }    
    }
}
