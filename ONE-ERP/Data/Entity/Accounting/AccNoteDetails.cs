using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccNoteDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int noteDetailsId { get; set; }
        public int? noteMasterId { get; set; }
        public AccNoteMaster noteMaster { get; set; }
        public int? ledgerId { get; set; }
        public AccLedgers ledger { get; set; }
        public int? sortOrder { get; set; }
    }
}
