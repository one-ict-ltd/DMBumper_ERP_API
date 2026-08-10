using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class UpdateTransferLog : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transferLogId { get; set; }
        public int? employeeId { get; set; }
        public string ZoneId { get; set; }
        public string DepoId { get; set; }
        public string RegionId { get; set; }
        public string AreaId { get; set; }
        public string TerritoryId { get; set; }
        public string PostingLocation { get; set; }
        public string prevZoneId { get; set; }
        public string prevDepoId { get; set; }
        public string prevRegionId { get; set; }
        public string prevAreaId { get; set; }
        public string prevTerritoryId { get; set; }
        public string prevPostingLocation { get; set; }
    }
}
