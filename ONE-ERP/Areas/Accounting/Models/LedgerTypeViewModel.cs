using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class LedgerTypeViewModel
    {
        public int? ledgerTypeId { get; set; }
        public string ledgerTypeName { get; set; }
        public bool? isActive { get; set; }

    }
}
