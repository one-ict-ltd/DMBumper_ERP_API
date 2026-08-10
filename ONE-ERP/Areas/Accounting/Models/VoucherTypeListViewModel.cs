using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherTypeListViewModel
    {
        public int? voucherTypeId { get; set; }
        public string voucherTypeName { get; set; }
        public string aliasName { get; set; }
        public int? isActive { get; set; }
        public string status { get; set; }
    }
}
