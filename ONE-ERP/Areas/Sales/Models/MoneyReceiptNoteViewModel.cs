using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class MoneyReceiptNoteViewModel
    {
        public int moneyReceiptId { get; set; }
        public string moneyReceiptNo { get; set; }
        public DateTime? moneyReceiptDate { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public string mioCode { get; set; }
        public string receivedFromPerson { get; set; }
        public string remarks { get; set; }
        public int? mrTypeId { get; set; }
        public decimal? amount { get; set; }
        public int? paymentModeId { get; set; }
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        public string trxNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
    }
}
