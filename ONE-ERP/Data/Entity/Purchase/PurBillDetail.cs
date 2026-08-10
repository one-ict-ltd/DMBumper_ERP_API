using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurBillDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int billDetailId { get; set; }
        public int? billMasterId { get; set; }
        public PurBillMaster billMaster { get; set; }
        public int? grnDetailId { get; set; }
        public PurGRNDetail grnDetail { get; set; }
        public decimal? receivedQty { get; set; }
        public decimal? rate { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? actualAmount { get; set; }
    }
}
