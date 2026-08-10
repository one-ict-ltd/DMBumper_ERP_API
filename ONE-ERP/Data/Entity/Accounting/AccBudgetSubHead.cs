using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccBudgetSubHead : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? budgetSubHeadId { get; set; }
        public int? budgetMainHeadId { get; set; }
        public AccBudgetMainHead budgetMainHead{get;set;}
        [MaxLength(50)]
        public string subHeadCode { get; set; }
        [MaxLength(250)]
        public string subHeadName { get; set; }
        public int? sortOrder { get; set; }
    }
}
