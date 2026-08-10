using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnMarketSchedules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarketScheduleID { get; set; }
        public int? RosterID { get; set; }
        public int? MarketID { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string Opinion { get; set; }
        public string MIOID { get; set; }
        public string ImageUrl { get; set; }
        public int CompanyID { get; set; }

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
        [MaxLength(50)]
        public string MioCode { get; set; }
        [MaxLength(50)]
        public string ScheduleNo { get; set; }

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
    }
}
