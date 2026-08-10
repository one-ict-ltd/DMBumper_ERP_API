using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ONEERP.Data.Entity.Sales
{
    public class MoneyReceiptType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int mrTypeId { get; set; }
        [MaxLength(250)]
        public string mrTypeName { get; set; }
    }
    public class MoneyReceiptNote : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int moneyReceiptId { get; set; }
        [MaxLength(250)]
        public string moneyReceiptNo { get; set; }
        [MaxLength(250)]
        public string moneyReceiptNoOld { get; set; }
        public DateTime? moneyReceiptDate { get; set; }
        [MaxLength(50)]
        public string depotCode { get; set; }
        [MaxLength(50)]
        public string territoryCode { get; set; }
        [MaxLength(50)]
        public string mioCode { get; set; }
        [MaxLength(250)]
        public string receivedFromPerson { get; set; }
        [MaxLength(250)]
        public string remarks { get; set; }
        public int? mrTypeId { get; set; }
        public MoneyReceiptType mrType { get; set; }
        public decimal? amount { get; set; }

        public int? paymentModeId { get; set; }
        public SalPaymentMode paymentMode { get; set; }
        [MaxLength(100)]
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        [MaxLength(100)]
        public string trxNo { get; set; }
        [MaxLength(100)]
        public string bankName { get; set; }
        [MaxLength(100)]
        public string branchName { get; set; }
    }
}
