using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostCentreAllocationViewModel
    {
        public int? costCentreAllocationId { get; set; }
        public int? costCentreId { get; set; }
        public int? voucherMasterId { get; set; }
        public int? voucherDetailId { get; set; }
        public int? ledgerId { get; set; }
        public int? partyId { get; set; } = 0;
        public decimal amount { get; set; }
        public bool? isActive { get; set; }

    }
}
