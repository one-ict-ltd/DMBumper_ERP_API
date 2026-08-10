using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class LedgersListViewModel
    {
        public int? ledgerId { get; set; }
        public int? accountGroupId { get; set; }
        public int? accountNatureId { get; set; }
        public int? printOrder { get; set; }
        public string natureName { get; set; }
        public string groupCode { get; set; }
        public string groupName { get; set; }
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
        public string status { get; set; }
    }
}
