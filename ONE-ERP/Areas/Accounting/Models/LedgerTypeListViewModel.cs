using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class LedgerTypeListViewModel
    {
        public int? ledgerTypeId { get; set; }
        public string ledgerTypeName { get; set; }
        public int? isActive { get; set; }
        public string status { get; set; }
    }
}
