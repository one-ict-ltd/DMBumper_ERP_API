using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.MasterData
{    
    public class SalaryWalletType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int walletTypeId { get; set; }
        [MaxLength(200)]
        public string walletTypeName { get; set; }    
    }
}
