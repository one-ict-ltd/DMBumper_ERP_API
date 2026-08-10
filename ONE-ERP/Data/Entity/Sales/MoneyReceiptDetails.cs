using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class MoneyReceiptDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int moneyReceiptDetailsId { get; set; }
        public int moneyReceiptNo { get; set; }
        public int? isSet { get; set; }
        public int? moneyReceiptMasterId { get; set; }
    }
}
