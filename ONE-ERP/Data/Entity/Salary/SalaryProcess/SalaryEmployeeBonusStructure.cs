using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.SalaryProcess
{
    public class SalaryEmployeeBonusStructure : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeBonusStructureId { get; set; }
        public int? bonusStructureId { get; set; }
        public SalaryBonusStructure bonusStructure { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }     
    }
}
