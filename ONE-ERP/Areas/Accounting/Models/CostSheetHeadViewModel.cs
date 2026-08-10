using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostSheetHeadViewModel
    {
        public int? costSheetHeadId { get; set; }
        public int? parentHeadId { get; set; }           
        public string costHeadName { get; set; }
        public string description { get; set; }
        public int? sortOrder { get; set; }
        public bool? isActive { get; set; }
        public int? isDetailsUpdated { get; set; }
        public List<CostSheetHeadAmountViewModel> lstDetails { get; set; }
    }
}
