namespace ONEERP.Areas.Sales.Models
{
    public class TerritoryCollectionTargetDetailsViewModel
    {
        public int terrColTargetDetailId { get; set; }

        public string territoryCode { get; set; }

        public decimal? targetAmount { get; set; }
        public int? terrColTargetMasterId { get; set; }
        public TerritoryCollectionTargetMasterViewModel terrColTargetMaster { get; set; }
    }
}
