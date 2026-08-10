using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherDetailViewModel
    {
        public int? voucherDetailsId { get; set; }
        public int? voucherMasterId { get; set; }
        public int? ledgerId { get; set; }
        public int? partyId { get; set; }
        public decimal? amount { get; set; }
        public int? transactionModeId { get; set; }
        public bool? isPrinAcc { get; set; }
        public bool? isActive { get; set; }
        public string accountName { get; set; }
        public string partyName { get; set; }
        public string remarksDetail { get; set; }
    }
}
