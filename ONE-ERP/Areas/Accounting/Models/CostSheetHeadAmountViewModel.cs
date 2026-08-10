using System;

namespace ONEERP.Areas.Accounting.Models
{
    public class CostSheetHeadAmountViewModel
    {
        public int? costSheetHeadAmountId { get; set; }
        public int? costSheetHeadId { get; set; }
        public int? formulaTypeId { get; set; }
        public int? ledgerId { get; set; }
        public bool? isActive { get; set; }
    }
}
