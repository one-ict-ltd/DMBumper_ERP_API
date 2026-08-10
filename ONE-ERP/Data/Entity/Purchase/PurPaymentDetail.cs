using ONEERP.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPaymentDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int paymentDetailId { get; set; }
        public int? paymentMasterId { get; set; } 
        public PurPaymentMaster paymentMaster { get; set; }
        public int? billDetailId { get; set; }
        public PurBillDetail billDetail { get; set; }
        public decimal? paymentAmount { get; set; }
        public decimal? bonusAmount { get; set; }
        public decimal? vatAdjustment { get; set; }
        public decimal? others { get; set; }

        [MaxLength(250)]
        public string bankName { get; set; }
        [MaxLength(250)]
        public string chequeNo { get; set; }
        [MaxLength(250)]
        public string trxNo { get; set; }
    }
}
