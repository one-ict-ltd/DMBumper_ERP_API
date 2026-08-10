using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salaryTypeId { get; set; }
        [MaxLength(100)]
        public string salaryTypeName { get; set; }    
    }
}
