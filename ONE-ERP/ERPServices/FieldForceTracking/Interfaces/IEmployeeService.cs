using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using ONEICT.Areas.Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployeeLoadViewModels();
        Task<EmployeeLoadJsonViewModel> GetEmployeeLoadJsonViewModels(string EmpId);

        Task<EmployeeLoadJsonViewModel> GetRXDetailsForEmployee(string EmpId, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag);
        Task<EmployeeLoadJsonViewModel> GetRXDetailsForDoctor(string EmpId, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag);
        Task<EmployeeLoadJsonViewModel> GetRXDetailsForItem(string EmpId, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag);

        Task<EmployeeLoadJsonViewModel> GetChemistJsonViewModels(string EmpId);
        Task<EmployeeLoadJsonViewModel> GetDoctorJsonViewModels(string EmpId);
        Task<bool> setEmployee(string EMP_ID, string EMPLYEE_NAME, string FATHER_NAME, string PRESENT_ADD, string PERMANENT_ADD, DateTime JOINING_DATE, int DESIGNATION, string MOBILE_NO, string EMAIL, string REMARKS,
          string EMP_STATUS, string BLOOD_GROUP, string NATIONAL_ID, string LAST_QUALIFICATION, string POSTING_LOCATION, string DEPOT_CODE, string ZONE_CODE, string REGION_CODE, string AREA_CODE, string TERRITORY_CODE, string ID, string EMP_TYPE);
        Task<EmployeeLoadJsonViewModel> GetallParam(string EmpId);
        Task<EmployeeLoadJsonViewModel> GetCheckinout(string EmpId);
        Task<bool> setCalender(string Id, int Day, DateTime Date, string DayName, int MonthNo, int Year, int IsHoliDay);
        Task<EmployeeLoadJsonViewModel> GetCheckinoutSummary(string EmpId);
        Task<EmployeeLoadJsonViewModel> GetCheckinoutHistory(string EmpId);
        Task<EmployeeLoadJsonViewModel> GetCheckinoutDetail(string EmpId, int year, int month);
        Task<EmployeeLoadJsonViewModel> GetCheckinoutDetailsummary(string EmpId, int year, int month);
        Task<EmployeeViewModel> GetEmployeeLoadViewModelsbyCode(string code);
        Task<JsonViewModel> GetMIOById(string code);
        Task<JsonViewModel> GetCustomerById(string MarketCode);
        Task<BrandListViewModel>  GetBrandList();
        Task<JsonViewModel> GetPriceRangeList(string BrandId);
        Task<EmployeeLoadJsonViewModel> GetEmployeegetallparamTerriLoadJson(string EmpId);
        Task<EmployeeLoadJsonViewModel> GetRXImage(string EmpId, DateTime fDate, DateTime tDate, string TerritoryCode, int DoctorId);
        Task<EmployeeLoadJsonViewModel> GetRXImageWithProduct(string EmpId, DateTime fDate, DateTime tDate, string TerritoryCode, int DoctorId, string productName, string skuName);
        Task<int> setTAAmountImages(int empId,int cmnTADAForEmployeeId, DateTime? taDate,  string imageUrl);
        Task<int> setTAAmount(int empId,DateTime? taDate,decimal? taAmount);

        #region New from SEBL-FFTS
        Task<EmployeeLoadJsonViewModel> GetCheckinoutDetailsevendays(string EmpId, int year, int month);
        Task<bool> setEmpExcel(string EMP_ID, string EMPLYEE_NAME, string FATHER_NAME, string MOBILE_NO, string PRESENT_ADD, string JOING_DATE, string POSTING_LOCATION, string ZONE_CODE, string DEPOT_CODE, string REGION_CODE, string AREA_CODE, string TERRITORY_CODE, string EMP_STATUS);
        Task<IEnumerable<EmployeeViewModel>> GetEmployeeLoadSViewModels();
        Task<bool> setEmpInOut(string EMP_ID, string INT_TIME, string OUT_TIME);
        Task<EmployeeLoadJsonViewModel> GetEmployeeDayadata(int year, int month);
        Task<bool> setEmpWeekend(List<WeekenDayEntryViewModel> lstData);
        Task<EmployeeLoadJsonViewModel> GetEmployeeDayadataday();
        Task<bool> setEmpWeekendday(List<WeekenDayEntryNViewModel> lstData);
        #endregion
    }
}
