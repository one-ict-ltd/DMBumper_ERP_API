using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class MoneyReceipt : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int moneyReceiptId { get; set; }
        public string receiptNo { get; set; }
        public DateTime? moneyReceiptDate { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public string mioCode { get; set; }
        public int? mrTypeId { get; set; }
        public string moneyReceiptBook { get; set; }
    }
}
