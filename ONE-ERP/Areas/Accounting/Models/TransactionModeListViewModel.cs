using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class TransactionModeListViewModel
    {
        public int? transactionModeId { get; set; }
        public string modeName { get; set; }
        public string sortOrder { get; set; }
        public int? isActive { get; set; }
        public string status { get; set; }
    }
}
