using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalTerritoryCollectionTargetDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int terrColTargetDetailId { get; set; }

        public string territoryCode { get; set; }

        public decimal? targetAmount { get; set; }
        public int? terrColTargetMasterId { get; set; }
        public SalTerritoryCollectionTargetMaster terrColTargetMaster { get; set; }
    }
}
