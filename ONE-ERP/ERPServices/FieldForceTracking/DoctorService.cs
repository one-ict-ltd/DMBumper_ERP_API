using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.MasterData
{
    public class DoctorService : IDoctorService
    {
        private readonly ERPDbContext _context;

        public DoctorService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDoctor(CmnDoctor cmnDoctor)
        {
            if (cmnDoctor.DoctorID != 0)
                _context.CmnDoctor.Update(cmnDoctor);
            else
                _context.CmnDoctor.Add(cmnDoctor);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CmnDoctor>> GetAllCmnDoctor()
        {
            try
            {
                return await _context.CmnDoctor.Where(x => x.IsActive == 1 && x.IsDeleted == 0).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<CmnDoctor> GetCmnDoctorById(int Id)
        {
            return await _context.CmnDoctor.Where(x => x.DoctorID == Id).AsNoTracking().FirstOrDefaultAsync();
        }      

        public async Task<JsonViewModel> DoctorListAPIViewModels(string Id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDoctorlist {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetDoctorId(string doctorNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDoctorId {doctorNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetDoctorListByUser(string Id,string employeeNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDoctorlistByUser {Id},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getDoctorlistByUserAndCode(string Id,string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDoctorlistByUserAndCode {Id},{code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> CmnDoctorForUpdateById(string Id,int doctorId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnDoctorForUpdateById {Id},{doctorId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> CmnDoctorRxwithproductById(string Id,int doctorId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnDoctorRxwithproductById {Id},{doctorId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DoctorListAPIViewModel>> DoctorListAPIbyMarketViewModels(string Id)
        {
            var result = await _context.doctorListAPIViewModels.FromSql($"getDoctorlistbymkt {Id}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<MarketListAPIViewModel>> MarketListAPIViewModels(string Id)
        {
            var result = await _context.marketListAPIViewModels.FromSql($"getMarketlist {Id}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<MarketListAPIPlanViewModel>> MarketListAPIPlanViewModels(string Id, string Date)
        {
            var result = await _context.marketListAPIPlanViewModels.FromSql($"getMarketPlanlist {Id},{Date}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<DoctorListViewModel>> GetDoctorListViewModel(string Id)
        {
            var result = await _context.doctorListViewModels.FromSql($"getDoctorlist {Id}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<bool> setDoctorchemistDeleteHistory(string Id, int DoctorDeleteHistoryID, int type, string doctorCode, string chemistCode,int status)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setDoctorchemistDeleteHistory {Id},{DoctorDeleteHistoryID},{type},{doctorCode},{chemistCode},{status}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> setDoctor(string Id, int DoctorID, string DoctorName, string Address, string Latitude, string Longitude, string MobileNo, string Speciality, string Institude, string Designation, string Degree, string NoOfPatient)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setDoctorData {Id},{DoctorID},{DoctorName},{Address},{Latitude},{Longitude},{MobileNo},{Speciality},{Institude},{Designation},{Degree},{NoOfPatient}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> setDoctorAPI(string Id, int DoctorID, string DoctorName, string Address, string Latitude, string Longitude, string MobileNo, string Speciality, string Institude, string Designation, string Degree, string NoOfPatient, string MarketID, string TerritoryID, string AreaId, string RegionId, string DepoId, string ZoneId,int DoctorCategoryId)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setDoctor {Id},{DoctorID},{DoctorName},{Address},{Latitude},{Longitude},{MobileNo},{Speciality},{Institude},{Designation},{Degree},{NoOfPatient},{MarketID},{TerritoryID},{AreaId},{RegionId},{DepoId},{ZoneId},{DoctorCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> SetsalesTarget(string Id, IncentiveCalculationViewModel model)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"CmnSpSetsalesTarget {Id},{model.IncentiveCalculationID},{model.employeeId},{model.territoryCode},{model.month},{model.year},{model.targetBudget},{model.superstarValueSales},{model.incentiveAmount},{model.achivementPercentage},{model.isActive},{model.achivementTargetBudget}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> setDoctorUnderObserbationAPI(string Id, int DoctorID, string DoctorName, string Address, string Latitude, string Longitude, string MobileNo, string Speciality, string Institude, string Designation, string Degree, string NoOfPatient,string TerritoryID,int DoctorCategoryId, DateTime? dateofBirth, DateTime? dateofMarrige, string favThings, int? practicePerMonth, decimal? honariumPerMonth, int? rxPerDay,int? rxPerMonth, string docDutyType, int? productId1, int? productId1RxPerDay, int? productId2, int? productId2RxPerDay, int? productId3, int? productId3RxPerDay, int? productId4, int? productId4RxPerDay, int? productId5, int? productId5RxPerDay, int? productId6, int? productId6RxPerDay, string chemberLocation,int? cmnDoctorId,int? status,string MarketCode,string MarketName, int? BasicDegreeId)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setDoctorUnderObserbation {Id},{DoctorID},{DoctorName},{Address},{Latitude},{Longitude},{MobileNo},{Speciality},{Institude},{Designation},{Degree},{NoOfPatient},{TerritoryID},{DoctorCategoryId},{dateofBirth},{dateofMarrige},{favThings},{practicePerMonth},{honariumPerMonth},{rxPerDay},{rxPerMonth},{docDutyType},{productId1},{productId1RxPerDay},{productId2},{productId2RxPerDay},{productId3},{productId3RxPerDay},{productId4},{productId4RxPerDay},{productId5},{productId5RxPerDay},{productId6},{productId6RxPerDay},{chemberLocation},{cmnDoctorId},{status},{MarketCode},{MarketName},{BasicDegreeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> setDoctor(DoctorListViewModel doctor, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setDoctorDataByAdmin {id},{doctor.doctorId},{doctor.doctorNo},{doctor.name},{doctor.address},{doctor.latitude},{doctor.longitude},{doctor.mobile},{doctor.speciality},{doctor.institute},{doctor.designation},{doctor.degree},{doctor.noOfPatient},{doctor.isActive},{doctor.marketId},{doctor.territoryId},{doctor.areaId},{doctor.regionId},{doctor.depoId},{doctor.zoneId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            var doctor = _context.CmnDoctor.Where(x => x.DoctorID == id).FirstOrDefault();
            doctor.IsDeleted = 1;
            _context.CmnDoctor.Attach(doctor);
            _context.Entry(doctor).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> setMarketAPI(string Id, int MarketId, string Name, string Address, string Latitude, string Longitude)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setMarketByAPI {Id},{MarketId},{Name},{Address},{Latitude},{Longitude}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteDoctorById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteDoctor {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetDoctorByTerritoryMarket(string MarketID, string TerritoryID)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetDoctorByTerritoryMarket {MarketID},{TerritoryID}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> setDoctorCategory(string Id, int DoctorCategoryID, string DoctorCategoryName, string DoctorCategoryCode,int IsActive)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"fftSpSetDoctorCategory {Id},{DoctorCategoryID},{DoctorCategoryName},{DoctorCategoryCode},{IsActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetDoctorCategory(int DoctorCategoryID,int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"fftGetDoctorCategory {DoctorCategoryID},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteDoctorCategoryById(string id, int catId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteDoctorCategory {id},{catId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> setDoctorRx(string Id, int DoctorRxID, int DoctorID, int productId,int productWiseSpecificationId,decimal quantity, int IsActive)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"fftSpSetDoctorRx {Id},{DoctorRxID},{DoctorID},{productId},{productWiseSpecificationId},{quantity},{IsActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetDoctorRx(int DoctorID)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"fftGetDoctorRx {DoctorID}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteDoctorRxById(string id, int DocId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteDoctorRx {id},{DocId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
