using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{
    public class SalaryBonusType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bonusTypeId { get; set; }
        [MaxLength(100)]
        public string bonusTypeName { get; set; }    
    }
}
