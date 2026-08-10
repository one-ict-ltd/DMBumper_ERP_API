using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DoctorSetParamViewModel
    {
       
        public string Id { get; set; }
        public string DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNo { get; set; }
        public string Speciality { get; set; }
        
        public string Institude { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public string NoOfPatient { get; set; }
        public string MarketCode { get; set; }

        public string MarketID { get; set; }
        public string TerritoryID { get; set; }
        public string AreaId { get; set; }
        public string RegionId { get; set; }
        public string DepoId { get; set; }
        public string ZoneId { get; set; }
        public int DoctorCategoryId { get; set; }
        public List<DoctorRxSetParamViewModel> lstDetailsViewModel { get; set; }
    }

    public class DoctorUnderObjervationViewModel
    {
        public string Id { get; set; }
        public string DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNo { get; set; }
        public string Speciality { get; set; }

        public string Institude { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public int? BasicDegreeId { get; set; }
        public string NoOfPatient { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public int DoctorCategoryId { get; set; }
        public int? status { get; set; }

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
        public int? cmnDoctorId { get; set; }
    }

    public class DoctorchemistDeleteHistoryViewModel
    {
        public int? DoctorDeleteHistoryID { get; set; }
        public int? type { get; set; } // 1 for doctor 2 chemist 
        public string doctorCode { get; set; }
        public string chemistCode { get; set; }
        public int? status { get; set; } //for approval 1= entry 2 = recomandaded 3 = approved 4 = rejected 
    }
}
