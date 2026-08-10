using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{    
    public class SalaryBonusStructure : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bonusStructureId { get; set; }
        public int? bonusSubRulesId { get; set; }
        public SalaryBonusSubRules bonusSubRules { get; set; }
        public int? salaryCalulationTypeId { get; set; }
        public SalaryCalulationType salaryCalulationType { get; set; }
        [MaxLength(20)]
        public string monthYearType { get; set; }
        [MaxLength(50)]
        public string bonusBasedOn { get; set; }        
        public int? minMonthValue { get; set; }
        public int? maxMonthValue { get; set; }
        public decimal? percentAmount { get; set; }
        public bool? hasEmployee { get; set; }

    }
}
