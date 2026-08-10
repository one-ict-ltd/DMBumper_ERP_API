using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class BudgetMasterViewModel
    {
        public int? budgetMasterId { get; set; }
        public int? fiscalYearId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public string budgetNo { get; set; }
        public DateTime budgetDate { get; set; }
        public decimal? grandTotal { get; set; }
        public int? status { get; set; }
        public bool? isActive { get; set; }
        public List<BudgetDetailsViewModel> lstdetailBudget { get; set; }
    }
}
