using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccVoucherDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int voucherDetailsId { get; set; }
        public int? voucherMasterId { get; set; }
        public AccVoucherMasters voucherMaster { get; set; }
        public int? ledgerId { get; set; }
        public AccLedgers ledger { get; set; }
        public int? partyId { get; set; }       
        public int? transactionModeId { get; set; }
        public AccTransactionMode transactionMode { get; set; }
        public decimal? amount { get; set; }        
        public bool? isPrinAcc { get; set; }
        [MaxLength(250)]
        public string accountName { get; set; }
        [MaxLength(250)]
        public string partyName { get; set; }
        public string remarks { get; set; }
    }
}
