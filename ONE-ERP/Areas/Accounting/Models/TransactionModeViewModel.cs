using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class TransactionModeViewModel
    {
        public int? transactionModeId { get; set; }
        public string modeName { get; set; }
        public int? sortOrder { get; set; }
        public bool? isActive { get; set; }

    }
}
