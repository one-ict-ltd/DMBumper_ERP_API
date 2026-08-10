
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Areas.Schedule.Models;
using ONEERP.Models;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IChemistScheduleService
    {
        //Task<bool> SaveChemistSchedule(CmnChemistSchedule cmnChemistSchedule);
        //Task<IEnumerable<CmnDoctor>> GetAllCmnDoctor();
        Task<bool> setPlanChemist(string Id, int RosterID, int ChemistID, DateTime visitDate, string VisitTime, string Opinion);
        Task<int> updatePlanChemist(string Id, int PlanID, string ImageUrl, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress, decimal? InvoiceAmount, decimal? CollectionAmount,int? paymentModeId,int ExecutionType,string territoryCode);
        //Task<int> CreateSalesOrderByChemist(string Id, DateTime? visitDate, int chemistId, ProductSubCatGetViewModel model, List<ProductSubCatGetViewModel> lstSalesModel);
        Task<int> setChemExecutionDetails(string Id, int ChemScheduleID, List<chemExecutionDetailsModel> ExecutionDetailsModel, string territoryCode);
        Task<int> SaveSalesOrderMasterByChemist(string id, ChemistSalesOrderCreateViewModel model);
        Task<int> SalesOrderDetailsByChemist(string userId, List<ProductSubCatGetViewModel> lstSalesModel, int salesInvoiceId);
        //Task<IEnumerable<ChemistScheduleListViewModel>> getChListAfterSetPlan(string Id, string VisitDate, int rosterID);
        Task<JsonViewModel> getChListAfterSetPlan(string Id, string VisitDate, int rosterID,string employeeNo);
        Task<IEnumerable<VisitReportChemistViewModel>> VisitReportChemistViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate);
        Task<IEnumerable<VisitReportDoctorViewModel>> VisitReportDoctorViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate);
        Task<IEnumerable<ChemistWiseVisitReportViewModel>> ChemistWiseVisitReportViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, string fromDate, string toDate);
        Task<IEnumerable<DoctorWiseVisitReportViewModel>> DoctorWiseVisitReportViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, string fromDate, string toDate);
        Task<IEnumerable<ChemistWiseVisitReportViewModel>> ChemistWiseVisitReportDViewModels(int Id, string fromDate, string toDate);       
        Task<IEnumerable<DoctorWiseVisitReportViewModel>> DoctorWiseVisitReportDViewModels(int Id, string fromDate, string toDate);
        Task<bool> PlanExecutionChemist(string Id, int RosterID, int ChemistID, int MarketID, string ImageUrl, DateTime VisitDate, string VisitTime, string Latitue, string Longitude, string Remarks, string LLAddress, decimal? InvoiceAmount, decimal? CollectionAmount);
        Task<IEnumerable<ChemistWiseVisitReportViewModel>> ChemistDataChartViewModels(int Id, string fromDate, string toDate);
        Task<IEnumerable<VisitReportEmployeeViewModel>> VisitReportEmployeeViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate);
        Task<bool> updatePlanChemiststartTime(string Id, int PlanID, string startTime, string Latitue, string Longitude);
        Task<JsonViewModel> GetMIODoctorVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string MIOName,string fromDate, string toDate);
        Task<JsonViewModel> GetChemistVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string EmpCode, string fromDate, string toDate);
        Task<JsonViewModel> GetChemistWiseVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string MarketName,int? ChemistId, string fromDate, string toDate);
        Task<JsonViewModel> GetDoctorWiseVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string MarketName,string CustomerName, string fromDate, string toDate);
        Task<bool> deletePlanChemist(string Id, int PlanId);
        Task<IEnumerable<StockSalesChartViewModel>> StockSalesChartViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date);
        Task<JsonViewModel> AttendanceViewModels(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date);
        Task<IEnumerable<StockSalesChartViewModel>> StockSalesChartViewModelsSale(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date);
        Task<IEnumerable<AttendenceReportViewModel>> AttendenceReportViewModels(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate);
        Task<bool> updateWeeklyPaln(string EMP_ID);
        Task<bool> setPlanExcel(string EmpCode, string Saturday, string StartTimeSaturday, string EndTimeSaturday, string RemarksSaturday, string Sunday, string StartTimeSunDay, string EndTimeSunday, string RemarksSunday, string Monday, string StartTimeMonDay, string EndTimeMonday, string RemarksMonday, string Tuesday, string StartTimeTuesDay, string EndTimeTuesday, string RemarksTuesday, string Wednesday, string StartTimeWednesDay, string EndTimeWednesday, string RemarksWednesday, string Thursday, string StartTimeThursDay, string EndTimeThursday, string RemarksThursday, string Friday, string StartTimeFriDay, string EndTimeFriday, string RemarksFriday);
        Task<bool> PlanProcess(string fromDate, string toDate, string EmpCode);
        Task<bool> updateWeeklyPalnDoc(string EMP_ID);
        Task<bool> setPlanDocExcel(string EmpCode, string Saturday, string StartTimeSaturday, string EndTimeSaturday, string RemarksSaturday, string Sunday, string StartTimeSunDay, string EndTimeSunday, string RemarksSunday, string Monday, string StartTimeMonDay, string EndTimeMonday, string RemarksMonday, string Tuesday, string StartTimeTuesDay, string EndTimeTuesday, string RemarksTuesday, string Wednesday, string StartTimeWednesDay, string EndTimeWednesday, string RemarksWednesday, string Thursday, string StartTimeThursDay, string EndTimeThursday, string RemarksThursday, string Friday, string StartTimeFriDay, string EndTimeFriday, string RemarksFriday);
        Task<bool> PlanProcessDoc(string fromDate, string toDate, string EmpCode);
        Task<bool> setDailyPlanDoc(string EmpCode, string DoctorCode, string day, string StartTime, string EndTime, string Remarks);

        Task<int> setRxUploadMaster(string Id, int rxUploadMasterID, int doctorId, DateTime date);
        Task<int> setRxUploadImage(string Id, int rxUploadMasterID, string imageUrl);
        Task<int> setRxUploadProduct(string Id, int rxUploadMasterID, int productId);
        Task<JsonViewModel> getTADAByEmployeeCode(string employeeCode);
        Task<JsonViewModel> getCmnWeeklyPlanDocByStatus(string Id,string employeeCode);
        Task<bool> updateDailyPlanDoc(int Id, int status);
        Task<JsonViewModel> getDashboardAttendanceDetails(string Id, string usertype, string type, string ZoneCode, string RegionCode, string AreaCode,DateTime date, string TerritoryCode);
        Task<JsonViewModel> getTADAReportByEmployeeCode(string Id);
        Task<JsonViewModel> getVehicleBillByEmployeeCode(string Id);
        Task<JsonViewModel> getEmployeeWiseVehicleBillByEmployeeCode(string employeeCode);
        Task<JsonViewModel> getEmployeeTADAByStatus(string Id);
        Task<bool> updateEmployeeTADA(int Id, int status,decimal? amount,string remarks);
        Task<JsonViewModel> getActionPlan(int userId); 
        Task<JsonViewModel> getExamContentById(int contentId);
        Task<JsonViewModel> getExamContent();
        Task<JsonViewModel> getExamContentNew();
        Task<JsonViewModel> getAllExamContent();
        Task<int> setExam(int employeeId, CmnExamQuestionViewModel model);
        Task<int> deleteExamContent(int employeeId, int examContentId);
        Task<JsonViewModel> getExamByContentId(int contentId, int employeeId);
        Task<JsonViewModel> getExamQuestionByexamId(int examId);
        Task<JsonViewModel> getExamResultByExamId(int examId);
        Task<int> setExamContent(int employeeId, ExamContentViewModel model);
        Task<int> setExamPerform(int employeeId, CmnExamPerformViewModel model);
        Task<JsonViewModel> GetExamResult(int employeeId, int status);
        Task<JsonViewModel> getGetExamResultByexamId(int employeeId, int examId, int status);
        Task<JsonViewModel> getActionCampain(int userId);
        Task<JsonViewModel> getEmployeeWiseTADAByEmployeeCode(string Id);
        Task<JsonViewModel> getEmployeeByRegionZoneTerritory(string Id);
        Task<bool> setDailyPlanChemist(string EmpCode, string DoctorCode, string day, string StartTime, string EndTime, string Remarks);
        Task<JsonViewModel> getCmnGetCmnWeeklyPlanChemistByStatus(string Id, string employeeCode);
        Task<bool> updateDailyPlanChemist(int Id, int status);
        Task<JsonViewModel> getTerritoryWiseMonthlyPromoItem(string Id,int monthNo);
        Task<bool> updateEmployeeMonthlyPromoItem(int Id, decimal? amount,int monthno);
        Task<JsonViewModel> getCmnDoctorUnderObserbationByStatus(string Id, string employeeCode, string RegionCode, string AreaCode);
        Task<bool> updateDoctorUnderObservation(int Id, int status);
        Task<bool> updatePartyUnderObservation(int Id, int status,decimal? creditLimit);
        Task<JsonViewModel> getKnowledgeSkill();
        Task<JsonViewModel> getAppsversion(int userId);
        Task<bool> setDailyPlanTerritory(string EmpCode, string territoryCode, string day, string StartTime, string EndTime, string Remarks);
        Task<JsonViewModel> getCmnweeklyplanterritoryByempCode(string Id, string employeeCode);
        Task<bool> updateDailyPlanTerritory(int Id, int status);
        Task<JsonViewModel> CmnweeklyplanterritoryApprovedToday(string Id, string employeeCode);
        Task<JsonViewModel> GetSetsalesTargetIdJson(int employeeId, int month, int year);
        Task<JsonViewModel> GetSetsalesTargetIdReportJson(int employeeId, int month, int year);
        Task<JsonViewModel> getEmployeeDataForMessage(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode);
        Task<JsonViewModel> getExamById(int examId, int employeeId);
        Task<JsonViewModel> GetExamQuestionSetByExamId(int examId, int employeeId);
        Task<int> setNoticeUploadImage(int Id, int UploadMasterID, int status, DateTime? fDate, DateTime? tDate, string imageUrl);
        Task<JsonViewModel> getDoctorBasicDegree(string Id);
        Task<JsonViewModelForTwoData> getDcrSummaryReport(int? userId, string ZoneId, string RegionId, string AreaId, string TerritoryID, DateTime fromDate, DateTime toDate, string reportId);
    }
}
