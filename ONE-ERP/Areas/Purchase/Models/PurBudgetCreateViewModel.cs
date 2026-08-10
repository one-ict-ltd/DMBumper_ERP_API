using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurBudgetCreateViewModel
    {
        public int BudgetCreateId { get; set; }
        public int BudgetCategoryId { get; set; }
        public decimal BudgetAmount { get; set; }
        public string BudgetYear { get; set; }
        public List<PurBudgetCreateViewModel> lstBudgetDetailsViewModel { get; set; }
    }

}
