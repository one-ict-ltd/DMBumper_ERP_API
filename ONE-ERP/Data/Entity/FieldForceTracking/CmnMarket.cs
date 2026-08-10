using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnMarket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int? TerritoryId { get; set; }
        [MaxLength(50)]
        public string ZoneCode { get; set; }
        [MaxLength(50)]
        public string DepotCode { get; set; }
        [MaxLength(50)]
        public string RegionCode { get; set; }
        [MaxLength(50)]
        public string AreaCode { get; set; }
        [MaxLength(50)]
        public string TerritoryCode { get; set; }
        public int? IsScheduled { get; set; }
        public string Address { get; set; }
        [MaxLength(50)]
        public string Latitude { get; set; }
        [MaxLength(50)]
        public string Longitude { get; set; }

        public int? IsActive { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string CreatePc { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdatePc { get; set; }
        public int? IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeleteOn { get; set; }
        public string DeletePc { get; set; }
        public string LLAddress { get; set; }

        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
