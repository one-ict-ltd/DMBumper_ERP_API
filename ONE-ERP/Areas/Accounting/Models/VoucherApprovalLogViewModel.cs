using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherApprovalLogViewModel
    {
        public int? voucherAppLogId { get; set; }
        public int? voucherMasterId { get; set; }
        public string remarks { get; set; }
        public int? isPosted { get; set; }
        public bool? isActive { get; set; }
    }
}
