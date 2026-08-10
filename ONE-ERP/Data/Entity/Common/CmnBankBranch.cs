using ONEERP.Data.Entity.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnBankBranch : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bankBranchId { get; set; }

        public int? bankId { get; set; }
        public CmnBank bank { get; set; }

        [MaxLength(250)]
        public string bankBranchName { get; set; }
        [MaxLength(500)]
        public string bankBranchAddress { get; set; }
        [MaxLength(50)]
        public string bankBranchCode { get; set; }

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