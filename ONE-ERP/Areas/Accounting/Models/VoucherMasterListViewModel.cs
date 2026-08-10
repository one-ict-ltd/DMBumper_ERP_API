using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherMasterListViewModel
    {
        public int? voucherMasterId { get; set; }
        public string voucherNo { get; set; }
        public DateTime? voucherDate { get; set; }
        public string refNo { get; set; }
        public int? voucherTypeId { get; set; }
        public string remarks { get; set; }
        public int? isPosted { get; set; }
        public decimal? amount { get; set; }
        public int? foundSourceId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? isActive { get; set; }
    }
}
