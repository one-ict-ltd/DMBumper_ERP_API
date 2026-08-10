using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class BudgetHeadMasterViewModel
    {
        public int? budgetHeadMasterId { get; set; }
        public int? budgetMainHeadId { get; set; }
        public int? budgetSubHeadId { get; set; }               
        public string headCode { get; set; }
        public string headName { get; set; }
        public int? sortOrder { get; set; }       
        public bool? isActive { get; set; }
        public List<BudgetHeadDetailsViewModel> lstdetailBudgetHead { get; set; }
    }
}
