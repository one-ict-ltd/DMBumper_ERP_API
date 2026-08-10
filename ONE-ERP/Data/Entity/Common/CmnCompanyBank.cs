using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnCompanyBank : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int companyBankId { get; set; }
        public string bankName { get; set; }
        public int? companyId { get; set; }
    }
}
