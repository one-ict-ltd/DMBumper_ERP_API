using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherStatusViewModel
    {
        public int? voucherStatusId { get; set; }
        public string statusName { get; set; }
        public bool? isActive { get; set; }

    }
}
