using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherStatusListViewModel
    {
        public int? voucherStatusId { get; set; }
        public string statusName { get; set; }
        public int? isActive { get; set; }
        public string status { get; set; }
    }
}
