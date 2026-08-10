using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Sales.Models
{
    public class MoneyReceiptViewModel
    {
        public int moneyReceiptId { get; set; }
        public string receiptNo { get; set; }
        public DateTime? moneyReceiptDate { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public string mioCode { get; set; }
        public int? mrTypeId { get; set; }
        public string moneyBook { get; set; }
        public List<MoneyReceiptDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
