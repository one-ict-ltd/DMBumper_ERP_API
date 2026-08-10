using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class OpeningBalanceListViewModel
    {
        public int? openingBalanceId { get; set; }
        public int? ledgerId { get; set; }
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public int? partyId { get; set; }
        public string partyCode { get; set; }
        public string partyName { get; set; }
        public int? transactionModeId { get; set; }
        public DateTime? balanceUpTo { get; set; }
        public decimal amount { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? isActive { get; set; }

    }
}
