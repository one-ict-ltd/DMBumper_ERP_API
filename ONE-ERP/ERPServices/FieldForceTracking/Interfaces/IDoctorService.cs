using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IDoctorService
    {
        Task<bool> SaveDoctor(CmnDoctor cmnDoctor);
        Task<IEnumerable<CmnDoctor>> GetAllCmnDoctor();
        Task<IEnumerable<DoctorListViewModel>> GetDoctorListViewModel(string Id);
        Task<CmnDoctor> GetCmnDoctorById(int Id);
        Task<bool> setDoctor(string Id, int DoctorID, string DoctorName, string Address, string Latitude, string Longitude, string MobileNo, string Speciality, string Institude, string Designation, string Degree, string NoOfPatient);
        Task<bool> setDoctor(DoctorListViewModel doctor, int id);
        Task<bool> DeleteDoctor(int id);
        Task<JsonViewModel> DoctorListAPIViewModels(string Id);
        Task<JsonViewModel> GetDoctorId(string doctorNo);
        Task<JsonViewModel> GetDoctorListByUser(string Id,string employeeNo);
        Task<bool> setDoctorAPI(string Id, int DoctorID, string DoctorName, string Address, string Latitude, string Longitude, string MobileNo, string Speciality, string Institude, string Designation, string Degree, string NoOfPatient, string MarketID, string TerritoryID, string AreaId, string RegionId, string DepoId, string ZoneId,int DoctorCategory);
        Task<bool> DeleteDoctorById(string id, int Id);
        Task<bool> setMarketAPI(string Id, int MarketId, string Name, string Address, string Latitude, string Longitude);
        Task<IEnumerable<MarketListAPIViewModel>> MarketListAPIViewModels(string Id);
        Task<IEnumerable<MarketListAPIPlanViewModel>> MarketListAPIPlanViewModels(string Id, string Date);
        Task<IEnumerable<DoctorListAPIViewModel>> DoctorListAPIbyMarketViewModels(string Id);
        Task<JsonViewModel> GetDoctorByTerritoryMarket(string MarketID, string TerritoryID);
        Task<bool> setDoctorCategory(string Id, int DoctorCategoryID, string DoctorCategoryName, string DoctorCategoryCode, int IsActive);
        Task<JsonViewModel> GetDoctorCategory(int DoctorCategoryID, int employeeId);
        Task<bool> DeleteDoctorCategoryById(string id, int catId);
        Task<bool> setDoctorRx(string Id, int DoctorRxID, int DoctorID, int productId, int productWiseSpecificationId, decimal quantity, int IsActive);
        Task<JsonViewModel> GetDoctorRx(int DoctorID);
        Task<bool> DeleteDoctorRxById(string id, int DocId);
        Task<bool> setDoctorUnderObserbationAPI(string Id, int DoctorID, string DoctorName, string Address, string Latitude, string Longitude, string MobileNo, string Speciality, string Institude, string Designation, string Degree, string NoOfPatient, string TerritoryID, int DoctorCategoryId, DateTime? dateofBirth, DateTime? dateofMarrige, string favThings, int? practicePerMonth, decimal? honariumPerMonth, int? rxPerDay, int? rxPerMonth, string docDutyType, int? productId1, int? productId1RxPerDay, int? productId2, int? productId2RxPerDay, int? productId3, int? productId3RxPerDay, int? productId4, int? productId4RxPerDay, int? productId5, int? productId5RxPerDay, int? productId6, int? productId6RxPerDay,string chemberLocation, int? cmnDoctorId,int? status, string MarketCode, string MarketName,int? BasicDegreeId);
        Task<bool> setDoctorchemistDeleteHistory(string Id, int DoctorDeleteHistoryID, int type, string doctorCode, string chemistCode, int status);
        Task<JsonViewModel> CmnDoctorForUpdateById(string Id, int doctorId);
        Task<JsonViewModel> CmnDoctorRxwithproductById(string Id, int doctorId);
        Task<JsonViewModel> getDoctorlistByUserAndCode(string Id, string code);
        Task<bool> SetsalesTarget(string Id, IncentiveCalculationViewModel model);
    }
}
