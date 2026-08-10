using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnChemistSchedules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChemistScheduleID { get; set; }
        public int? RosterID { get; set; }
        public int ChemistID { get; set; }
        public DateTime? VisitDate { get; set; }
        [MaxLength(50)]
        public string VisitTime { get; set; }
        public string Opinion { get; set; }
        [MaxLength(150)]
        public string MIOID { get; set; }
        [MaxLength(350)]
        public string ImageUrl { get; set; }
        [MaxLength(50)]
        public string ExecuteTime { get; set; }
        [MaxLength(50)]
        public string Latitude { get; set; }
        [MaxLength(50)]
        public string Longitude { get; set; }
        public string Remarks { get; set; }
        public int? CompanyID { get; set; }
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
        public decimal? InvoiceAmount { get; set; }
        public decimal? CollectionAmount { get; set; }
        [MaxLength(50)]
        public string EndTime { get; set; }
        [MaxLength(50)]
        public string StartTime { get; set; }
        [MaxLength(50)]
        public string TerritoryCode { get; set; }
        [MaxLength(50)]
        public string AreaCode { get; set; }
        [MaxLength(50)]
        public string RegionCode { get; set; }
        [MaxLength(50)]
        public string DepotCode { get; set; }
        [MaxLength(50)]
        public string ZoneCode { get; set; }
        public int? IsExecuted { get; set; }
        public int? paymentModeId { get; set; }
        public int ExecutionType { get; set; }
    }
}
