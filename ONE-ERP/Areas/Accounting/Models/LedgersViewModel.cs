using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class LedgersViewModel
    {
        public int? ledgerId { get; set; }
        public int? accountGroupId { get; set; }
        public int? accountNatureId { get; set; }
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public string aliasName { get; set; }
        public int? haveSubledger { get; set; }
        public int? currencyId { get; set; }
        public int? parentId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
        public int? ledgerTypeId { get; set; }
        public int? haveCostCentre { get; set; }
        public string ledgerPrefix { get; set; }
        public int? noteId { get; set; }
    }
}
