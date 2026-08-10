using ONEERP.Data.Entity.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnBank: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bankId { get; set; }
        public int? bankTypeId { get; set; }
        public CmnBankType bankType { get; set; }
        [MaxLength(250)]
        public string bankName { get; set; } 
        
        public int? acchhledgerId { get; set; }
        public AccLedgers acchhledger { get; set; }
        public int? accahledgerId { get; set; }
        public AccLedgers accahledger { get; set; }

        public int? acchhcrledgerId { get; set; }
        public AccLedgers acchhcrledger { get; set; }
        public int? accahcrledgerId { get; set; }
        public AccLedgers accahcrledger { get; set; }
    }
}
