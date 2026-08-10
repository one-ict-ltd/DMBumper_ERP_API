using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccBudgetHeadMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int budgetHeadMasterId { get; set; }
        public int budgetMainHeadId { get; set; }
        public AccBudgetMainHead budgetMainHead { get; set; }
        public int budgetSubHeadId { get; set; }
        public AccBudgetSubHead budgetSubHead { get; set; }
        [MaxLength(50)]
        public string headCode { get; set; }
        [MaxLength(250)]
        public string headName { get; set; }
        public int? sortOrder { get; set; }
    }
}
