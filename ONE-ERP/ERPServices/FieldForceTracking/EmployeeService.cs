using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Models;
using ONEICT.Areas.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ERPDbContext _context;

        public EmployeeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeeLoadViewModels()
        {
            try
            {
                var result = await _context.employeeLoadViewModels.FromSql($"getEMP").AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EmployeeViewModel> GetEmployeeLoadViewModelsbyCode(string code)
        {
            try
            {
                var result = await _context.employeeLoadViewModels.FromSql($"getEMPByCode {code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EmployeeLoadJsonViewModel> GetEmployeeLoadJsonViewModels(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getEMPDetail {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetRXDetailsForEmployee(string EmpId, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode,string territoryCode,string flag)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"CmnSpGetRXDetailsByEmployee {EmpId}, {fDate},{tDate},{zoneCode},{regionCode},{areaCode},{territoryCode},{flag}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetRXDetailsForDoctor(string EmpId, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"CmnSpGetRXDetailsByDoctor {EmpId}, {fDate},{tDate},{zoneCode},{regionCode},{areaCode},{territoryCode},{flag}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetRXImage(string EmpId, DateTime fDate, DateTime tDate, string TerritoryCode, int DoctorId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"CmnSpGetRXImage {EmpId}, {fDate},{tDate},{TerritoryCode},{DoctorId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetRXImageWithProduct(string EmpId, DateTime fDate, DateTime tDate, string TerritoryCode, int DoctorId,string productName, string skuName)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"CmnSpGetRXImageWithProduct {EmpId}, {fDate},{tDate},{TerritoryCode},{DoctorId},{productName},{skuName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetRXDetailsForItem(string EmpId, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"CmnSpGetRXDetailsByItem {EmpId}, {fDate},{tDate},{zoneCode},{regionCode},{areaCode},{territoryCode},{flag}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetEmployeegetallparamTerriLoadJson(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getallparamTerri {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetChemistJsonViewModels(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getChemistDetail {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetallParam(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getallparam {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetCheckinout(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getCheckinOut {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetCheckinoutSummary(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getattendsummarydata {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetCheckinoutHistory(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getattendhistory {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetCheckinoutDetail(string EmpId, int year, int month)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getCheckinOutDetail {EmpId},{year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetCheckinoutDetailsummary(string EmpId, int year, int month)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getCheckinOutDetailsummary {EmpId},{year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<EmployeeLoadJsonViewModel> GetDoctorJsonViewModels(string EmpId)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getDoctorDetail {EmpId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> setEmployee(string EMP_ID, string EMPLYEE_NAME, string FATHER_NAME, string PRESENT_ADD, string PERMANENT_ADD, DateTime JOINING_DATE, int DESIGNATION, string MOBILE_NO, string EMAIL, string REMARKS,
          string EMP_STATUS, string BLOOD_GROUP, string NATIONAL_ID, string LAST_QUALIFICATION, string POSTING_LOCATION, string DEPOT_CODE, string ZONE_CODE, string REGION_CODE, string AREA_CODE, string TERRITORY_CODE, string ID, string EMP_TYPE)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setEMP {EMP_ID},{EMPLYEE_NAME},{FATHER_NAME},{PRESENT_ADD},{PERMANENT_ADD},{JOINING_DATE},{DESIGNATION},{MOBILE_NO},{EMAIL},{REMARKS},{EMP_STATUS},{BLOOD_GROUP},{NATIONAL_ID},{LAST_QUALIFICATION},{POSTING_LOCATION},{DEPOT_CODE},{ZONE_CODE},{REGION_CODE},{AREA_CODE},{TERRITORY_CODE},{ID},{EMP_TYPE}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<bool> setCalender(string Id, int Day, DateTime Date, string DayName, int MonthNo, int Year, int IsHoliDay)
        {
            var result = await _context.saveScheduleViewModels.FromSql($"setCalender {Id},{Day},{Date},{DayName},{MonthNo},{Year},{IsHoliDay}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMIOById(string code)
        {
            var result = await _context.jsonViewModels.FromSql($"geMIO {code}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetCustomerById(string MarketCode)
        {
            var result = await _context.jsonViewModels.FromSql($"getCustomerMarketCodeWise {MarketCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<BrandListViewModel> GetBrandList()
        {
            var result = await _context.brandListViewModels.FromSql($"getBrand").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPriceRangeList(string BrandId)
        {
            var result = await _context.jsonViewModels.FromSql($"getPriceCat {BrandId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> setTAAmountImages(int empId, int CmnTADAForEmployeeId, DateTime? taDate, string imageUrl)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"setTAAmountImages {empId},{CmnTADAForEmployeeId},{taDate},{imageUrl}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> setTAAmount(int empId, DateTime? taDate, decimal? taAmount)
        {
            try
            {
                // var query = $"setTAAmount {empId},{taDate},{taAmount}";
                var result = await _context.saveUpdateValueViewModels.FromSql($"setTAAmount {empId},{taDate},{taAmount}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }


        #region New from SEBL-FFTS


        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeeLoadSViewModels()
        {
            try
            {
                var result = await _context.employeeLoadViewModels.FromSql($"getEMP").AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EmployeeLoadJsonViewModel> GetCheckinoutDetailsevendays(string EmpId, int year, int month)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getCheckinOutDetailsevendays {EmpId},{year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> setEmpExcel(string EMP_ID, string EMPLYEE_NAME, string FATHER_NAME, string MOBILE_NO, string PRESENT_ADD, string JOING_DATE, string POSTING_LOCATION, string ZONE_CODE, string DEPOT_CODE, string REGION_CODE, string AREA_CODE, string TERRITORY_CODE, string EMP_STATUS)
        {
            string date = null;
            if (JOING_DATE != null)
            {
                date = Convert.ToDateTime(JOING_DATE).ToString("yyyy-MM-dd");
            }

            // date = Convert.ToDateTime(JOING_DATE).ToString("yyyy-MM-dd");
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setEmpexcel {EMP_ID},{EMPLYEE_NAME},{FATHER_NAME},{MOBILE_NO},{PRESENT_ADD},{date},{POSTING_LOCATION},{ZONE_CODE},{DEPOT_CODE},{REGION_CODE},{AREA_CODE},{TERRITORY_CODE},{EMP_STATUS}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }
        public async Task<bool> setEmpInOut(string EMP_ID, string INT_TIME, string OUT_TIME)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setEmpCheckINOut {EMP_ID},{INT_TIME},{OUT_TIME}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }
        public async Task<bool> setEmpWeekend(List<WeekenDayEntryViewModel> lstData)
        {
            try
            {
                var result = new SaveScheduleViewModel();
                foreach (WeekenDayEntryViewModel data in lstData)
                {
                    result = await _context.saveScheduleViewModels.FromSql($"setEmpWeekend {data.EMP_ID},{data.Date},{data.month},{data.year},{data.isHoliDay}").AsNoTracking().FirstOrDefaultAsync();
                }

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }
        public async Task<bool> setEmpWeekendday(List<WeekenDayEntryNViewModel> lstData)
        {
            try
            {
                var result = new SaveScheduleViewModel();
                foreach (WeekenDayEntryNViewModel data in lstData)
                {
                    result = await _context.saveScheduleViewModels.FromSql($"setEmpWeekendday {data.EMP_ID},{data.friDay},{data.saturDay},{data.sunDay},{data.monDay},{data.tuesDay},{data.wednesDay},{data.thrusDay}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<EmployeeLoadJsonViewModel> GetEmployeeDayadata(int year, int month)
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getdatewisedata {year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeLoadJsonViewModel> GetEmployeeDayadataday()
        {
            var result = await _context.employeeLoadJsonViewModels.FromSql($"getdatewisedataday").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion

    }
}
