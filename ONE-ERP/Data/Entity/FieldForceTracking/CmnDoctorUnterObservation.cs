using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDoctorUnterObservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorID { get; set; }
        public string DoctorNo { get; set; }
        public string DoctorName { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNo { get; set; }
        public string Speciality { get; set; }
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

        public int? IsScheduled { get; set; }
        public int? OplCatgeoryId { get; set; }
        public string Degree { get; set; }
        public int? BasicDegreeId { get; set; }
        public CmnBasicDegree BasicDegree { get; set; }
        public string Designation { get; set; }
        public string Institude { get; set; }
        public string NoOfPatient { get; set; }
        [MaxLength(50)]
        public string MarketID { get; set; }
        [MaxLength(50)]
        public string TerritoryID { get; set; }
        [MaxLength(50)]
        public string AreaId { get; set; }
        [MaxLength(50)]
        public string RegionId { get; set; }
        [MaxLength(50)]
        public string DepoId { get; set; }
        [MaxLength(50)]
        public string ZoneId { get; set; }
        [MaxLength(50)]
        public string OplPotential { get; set; }
        public int? CompanyId { get; set; }

        public int? status { get; set; } //for approval 1= entry 2 = recomandaded 3 = approved 4 = rejected 

        public DateTime? dateofBirth { get; set; }
        public DateTime? dateofMarrige { get; set; }
        public string favThings { get; set; }
        public int? practicePerMonth { get; set; }
        public decimal? honariumPerMonth { get; set; }
        public int? rxPerDay { get; set; }
        public int? rxPerMonth { get; set; }
        public string docDutyType { get; set; }
        public int? productId1 { get; set; }
        public int? productId1RxPerDay { get; set; }
        public int? productId2 { get; set; }
        public int? productId2RxPerDay { get; set; }
        public int? productId3 { get; set; }
        public int? productId3RxPerDay { get; set; }
        public int? productId4 { get; set; }
        public int? productId4RxPerDay { get; set; }
        public int? productId5 { get; set; }
        public int? productId5RxPerDay { get; set; }
        public int? productId6 { get; set; }
        public int? productId6RxPerDay { get; set; }
        public string chemberLocation { get; set; }

        public int? cmnDoctorId { get; set; } //if exist for update
    }
}
