using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IDoctorScheduleService
    {
      //  Task<bool> SaveDoctorSchedule(CmnDoctorSchedule cmnDoctorSchedule);
        //Task<IEnumerable<CmnDoctor>> GetAllCmnDoctor();
        Task<bool> setPlanDoctor(string Id, int RosterID, int DoctorID, DateTime visitDate, string VisitTime, string Opinion);
        Task<int> updatePlanDoctor(string Id, int PlanID, string ImageUrl, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress, int ExecutionType,string territoryCode,int DoctorId);
        Task<int> setDocExecutionDetails(string Id, int DoctorScheduleID, List<docExecutionDetailsModel> ExecutionDetailsModel,string territoryCode);
        Task<bool> UpdatePlanDoctorstartTime(string Id, int PlanID, string startTime, string Latitue, string Longitude);
        //Task<IEnumerable<DoctorScheduleListViewModel>> getDrListAfterSetPlan(string Id, string VisitDate, int rosterID);
        Task<JsonViewModel> getDrListAfterSetPlan(string Id, string VisitDate, int rosterID,string employeeNo);
        Task<bool> setCurrentLocation(string Id, string Latitude, string Longitude, string Address, string DateTime);
        //Task<bool> setCheckInOut(string Id, string Latitude, string Longitude, string DateTime, int Flag);
        Task<bool> setCheckInOut(string Id, string Latitude, string Longitude, string DateTime, int Flag, string address, string opinion, string time, bool? isHQ, bool? isEHQ, bool? isOS, bool? isOther);
        //Task<bool> setPlanMarket(string Id, int RosterID, int MarketID, DateTime visitDate, string VisitTime, string Opinion);
        Task<bool> setPlanMarket(string Id, int RosterID, int MarketID, DateTime visitDate, string VisitTime, string Opinion, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MioCode);
        Task<bool> PlanExecutionDoctor(string Id, int RosterID, int DoctorID, int MarketID, string ImageUrl, DateTime VisitDate, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress);
        Task<EmployeeLoadJsonViewModel> GetMarketScheduleJsonViewModels(string Id, string Date, int RosterId);
        Task<EmployeeLoadJsonViewModel> GetEmployeeDynamicJsonViewModels(string Code, string Type, string EmpCode);
        Task<EmployeeLoadJsonViewModel> GetDoctorsDynamicJsonViewModels(string Code, string Type);
        Task<EmployeeLoadJsonViewModel> GetChemistsDynamicJsonViewModels(string Code, string Type);
        Task<bool> PlanExecutionEmp(string Id, int RosterID, string EmpCode, int MarketID, string ImageUrl, DateTime VisitDate, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress);
        Task<EmployeeLoadJsonViewModel> GetEmployeeReportDynamicJsonViewModels(string Code, string CodeType, string Type, string EmpCode);
        Task<bool> deletePlanDoctor(string userId, int PlanId);
        Task<JsonViewModel> getDashBoardPlanApp(string Id,DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode);
        Task<JsonViewModel3> getDashBoardPlanApp3(string Id,DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode);
        Task<JsonViewModel4> getDashBoardPlanApp4(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode);
        Task<JsonViewModel4> getDashBoardPlanApp5(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode);
        Task<JsonViewModel> getDashBoardPlanAppDaily(string Id, DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode);
        Task<JsonViewModel> getDashBoardAttnApp(string Id,DateTime date, string ZoneCode, string RegionCode, string AreaCode, string TerritoryCode);
    }
}
