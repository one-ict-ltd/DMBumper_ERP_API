using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherApprovalLogListViewModel
    {

        public int? voucherAppLogId { get; set; }
        public int? voucherMasterId { get; set; }
        public string voucherNo { get; set; }
        public string refNo { get; set; }
        public DateTime? voucherDate { get; set; }
        public DateTime? createdAt { get; set; }
        public string remarks { get; set; }
        public string statusName { get; set; }
        public int? voucherTypeId { get; set; }
        public int? voucherStatusId { get; set; }
    }
}
