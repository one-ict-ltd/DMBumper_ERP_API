using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccUserWiseLedger:NewBase 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userWiseLedgerId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }

        public int?  ledgerId { get; set; }
        public AccLedgers  ledger { get; set; }

        public string depotCode { get; set; }
    }
}
