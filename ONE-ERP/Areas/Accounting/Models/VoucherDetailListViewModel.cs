using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherDetailListViewModel
    {
        public int? voucherDetailsId { get; set; }
        public int? voucherMasterId { get; set; }
        public string voucherNo { get; set; }
        public string refNo { get; set; }
        public DateTime? voucherDate { get; set; }
        public int? voucherTypeId { get; set; }
        public string remarks { get; set; }
        public int? ledgerId { get; set; }
        public int? partyId { get; set; }
        public decimal? amount { get; set; }
        public int? transactionModeId { get; set; }
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public string partyCode { get; set; }
        public string partyName { get; set; }
        public int? isPrinAcc { get; set; }
        public int? isActive { get; set; }
    }
}
