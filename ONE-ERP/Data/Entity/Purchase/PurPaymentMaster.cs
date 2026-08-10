using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPaymentMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int paymentMasterId { get; set; }
        [MaxLength(20)]
        public string paymentNumber { get; set; }
        public string referenceNo { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
      
        public int? billMasterId { get; set; }
        public PurBillMaster billMaster { get; set; }
        public DateTime? paymentDate { get; set; }
        public decimal? paymentAmount { get; set; }
        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public SalPaymentMode paymentMode { get; set; }

        [MaxLength(250)]
        public string bankName { get; set; }
        [MaxLength(250)]
        public string branchName { get; set; }
        [MaxLength(50)]
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        [MaxLength(250)]
        public string trxNo { get; set; }

        public int? voucherMasterId { get; set; }
        public AccVoucherMasters voucherMaster { get; set; }
    }
}
