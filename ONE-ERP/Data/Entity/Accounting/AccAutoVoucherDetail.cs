using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccAutoVoucherDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int autoVoucherDetailId { get; set; }
        public int? autoVoucherMasterId { get; set; }
        public AccAutoVoucherMaster autoVoucherMaster { get; set; }
        public int? transactionModeId { get; set; }
        public AccTransactionMode transactionMode{get;set;}
        public int? ledgerId { get; set; }
        public AccLedgers ledger { get; set; }
    }
}
