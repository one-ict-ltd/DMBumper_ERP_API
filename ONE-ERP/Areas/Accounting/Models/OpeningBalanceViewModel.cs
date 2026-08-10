using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class OpeningBalanceViewModel
    {
        public int? openingBalanceId { get; set; }
        public int? ledgerId { get; set; }
        public int? partyId { get; set; }
        public int? transactionModeId { get; set; }
        public DateTime? balanceUpTo { get; set; }
        public decimal amount { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
        public string description { get; set; }
    }
}
