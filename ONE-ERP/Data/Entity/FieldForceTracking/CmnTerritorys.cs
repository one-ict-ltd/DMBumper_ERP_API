using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnTerritorys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeritoryID { get; set; }
        public string ZoneCode { get; set; }
        public string DepotCode { get; set; }
        public string RegionCode { get; set; }
        public string AreaCode { get; set; }
        public string TerritoryCode { get; set; }
        public string TerritoryName { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public int? sortOrder { get; set; }
        [MaxLength(50)]
        public string SquareRefCode { get; set; }
        public double? salesLimit { get; set; }
        public string mobileNo { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        [MaxLength(250)]
        public string createdBy { get; set; }
        [MaxLength(250)]
        public string updatedBy { get; set; }
    }
}
