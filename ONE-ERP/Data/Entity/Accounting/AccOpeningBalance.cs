using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccOpeningBalance:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int openingBalanceId { get; set; }
        public int? ledgerId { get; set; }
        public AccLedgers ledger { get; set; }
        public int? partyId { get; set; }        
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? transactionModeId { get; set; }
        public AccTransactionMode transactionMode { get; set; }
        public DateTime? balanceUpTo { get; set; }
        public decimal amount { get; set; }       

        public int? departmentId { get; set; }
        public string description { get; set; }

    }
}
