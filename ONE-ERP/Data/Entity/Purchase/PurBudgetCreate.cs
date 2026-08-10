using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurBudgetCreate : NewBase  
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BudgetCreateId { get; set; }
        public int BudgetCategoryId { get; set; }
        public decimal BudgetAmount { get; set; }
        public string BudgetYear { get; set; }
    }
}
