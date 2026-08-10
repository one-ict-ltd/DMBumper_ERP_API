using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalPaymentMode:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int paymentModeId { get; set; }
        [MaxLength(250)]
        public string paymentMode { get; set; }
        public int? paymentTypeId { get; set; }
        public SalPaymentType paymentType { get; set; }
    }
}
