using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCostCentreAllocation:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int costCentreAllocationId { get; set; }
        public int? costCentreId { get; set; }
        public AccCostCentre costCentre { get; set; }
        public int? voucherMasterId { get; set; }
        public AccVoucherMasters voucherMaster { get; set; }
        public int? voucherDetailId { get; set; }
        public AccVoucherDetails voucherDetail { get; set; }
        public decimal? amount { get; set; }
    }
}
