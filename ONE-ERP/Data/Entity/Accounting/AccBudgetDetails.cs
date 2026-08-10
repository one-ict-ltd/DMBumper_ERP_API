using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccBudgetDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int budgetDetailsId { get; set; }
        public int? budgetMasterId { get; set; }
        public AccBudgetMaster budgetMaster { get; set; }
        public int? budgetHeadMasterId { get; set; }
        public AccBudgetHeadMaster budgetHeadMaster { get;set;}
        public decimal? firstMonth { get; set; }
        public decimal? secondMonth { get; set; }
        public decimal? thirdMonth { get; set; }
        public decimal? fourthMonth { get; set; }
        public decimal? fifthMonth { get; set; }
        public decimal? sixthMonth { get; set; }
        public decimal? seventhMonth { get; set; }
        public decimal? eighthMonth { get; set; }
        public decimal? ninethMonth { get; set; }
        public decimal? tenthMonth { get; set; }
        public decimal? eleventhMonth { get; set; }
        public decimal? twelvethMonth { get; set; }
        public decimal? subTotal { get; set; }
    }
}
