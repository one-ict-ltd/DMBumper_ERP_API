using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class AccountGroupViewModel
    {
        public int? accountGroupId { get; set; }
        public int? parentId { get; set; }
        public int? groupNatureId { get; set; }
        public string groupCode { get; set; }
        public string groupName { get; set; }
        public bool? isActive { get; set; }
        public int? sortOrder { get; set; }
    }

    public class UserWiseLedgerViewModel
    {
        public int? employeeId { get; set; }

        public List<LedgerListViewModel> lstModel { get; set; }
    }

    public class LedgerListViewModel
    {
        public int ledgerId { get; set; }
        public bool isActive { get; set; }
        public string accountName { get; set; }
    }
}
