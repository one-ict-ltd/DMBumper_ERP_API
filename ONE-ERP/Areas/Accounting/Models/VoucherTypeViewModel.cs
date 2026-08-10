using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherTypeViewModel
    {
        public int? voucherTypeId { get; set; }
        public string voucherTypeName { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }

    }
}
