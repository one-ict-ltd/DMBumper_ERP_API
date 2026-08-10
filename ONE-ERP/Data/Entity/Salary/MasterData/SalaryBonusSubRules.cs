using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryBonusSubRules : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bonusSubRulesId { get; set; }
        public int? bonusRulesId { get; set; }
        public SalaryBonusRules bonusRules { get; set; }
        [MaxLength(250)]
        public string bonusSubRulesName { get; set; }
        
    }
}
