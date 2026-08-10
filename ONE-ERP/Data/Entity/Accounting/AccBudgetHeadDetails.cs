using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccBudgetHeadDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int budgetHeadDetailsId { get; set; }
        public int? budgetHeadMasterId { get; set; }
        public AccBudgetHeadMaster budgetHeadMaster { get; set; }
        public int? ledgerId { get; set; }
        public AccLedgers ledger { get; set; }
    }
}
