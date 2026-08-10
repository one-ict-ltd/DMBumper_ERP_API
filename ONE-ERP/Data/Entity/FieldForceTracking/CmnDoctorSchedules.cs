using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDoctorSchedules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorScheduleID { get; set; }
        public int? RosterID { get; set; }
        public int? DoctorID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string Opinion { get; set; }
        public string MIOID { get; set; }
        public string ImageUrl { get; set; }
        public string ExecuteTime { get; set; }
        public string Latitude { get; set; }
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
        [MaxLength(50)]
        public string EndTime { get; set; }
        [MaxLength(50)]
        public string StartTime { get; set; }
        public int? IsExecuted { get; set; }
        public int ExecutionType { get; set; }
        public string territoryCode { get; set; }

    }
}
