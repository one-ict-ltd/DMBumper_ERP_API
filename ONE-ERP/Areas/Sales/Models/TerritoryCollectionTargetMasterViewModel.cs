using System.Collections.Generic;
using System;

namespace ONEERP.Areas.Sales.Models
{
    public class TerritoryCollectionTargetMasterViewModel
    {
        public int terrColTargetMasterId { get; set; }
        public string depotCode { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public List<TerritoryCollectionTargetDetailsViewModel> terrColTargetDetails { get; set; }
    }
}
