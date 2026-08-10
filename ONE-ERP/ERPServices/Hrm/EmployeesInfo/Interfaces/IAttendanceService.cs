using ONEERP.Areas.Attendance.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces
{
    public interface IAttendanceService
    {
        #region Calender

        Task<bool> SaveCalender(string userId, List<CalenderViewModel> calenderViewModels, CalenderViewModel model);
        Task<JsonViewModel> GetCalender();
        Task<JsonViewModel> GetCalenderByMonth(int year, int month);
        Task<bool> DeleteCalenderByMonth(string userId, CalenderViewModel model);
        #endregion

        #region ShiftGroup Master
        Task<int> SaveShiftGroupMaster(string userId, ShiftGroupMasterViewModel model);
        Task<JsonViewModel> GetShiftGroupMasterById(int shiftMasterId);
        Task<JsonViewModel> GetDuplicateShiftGroupMaster(int shiftMasterId, string shiftName);
        Task<bool> DeleteShiftGroupMasterById(string userId, int shiftMasterId);

        #endregion

        #region  ShiftGroup Detail
        Task<int> SaveShiftGroupDetail(string userId, List<ShiftGroupDetailViewModel> shiftGroupDetailViewModels, int shiftMasterId);
        Task<JsonViewModel> GetShiftGroupDetailByMasterId(int shiftMasterId);

        #endregion

        #region Assign Shift & Update PunchCard 
        Task<int> AssignShiftGroup(string userId, PunchCardViewModel model);
        Task<JsonViewModel> GetShiftAssignById(int punchCardId, int companyId, int sbuId, int employeeId, string department);
        Task<JsonViewModel> GetPunchCardById(int punchCardId);
        Task<bool> DeletePunchCardById(string userId, int punchCardId);
        Task<bool> UpdatePunchCardNo(string userId, PunchCardViewModel model);

        #endregion

        #region Attendance Process

        Task<bool> ProcessAttendance(string userId, DateTime startDate, DateTime endDate, int companyId);
        Task<JsonViewModel> GetAttendanceByDate(DateTime startDate, DateTime endDate, int companyId, int employeeId);
        Task<JsonViewModel> GetEmployeeAttnClarificationById(int employeecClarificationId);
        Task<bool> SaveEmployeeAttnClarification(string userId, int empId, int employeecClarificationId,DateTime AttendanceDate, string clarification);
        Task<JsonViewModel> GetDuplicateAttendanceDateForClarification(int employeecClarificationId, DateTime? attendanceDate, int empId);
        Task<JsonViewModel> HrmSpGetEmployeeClarificationForApprovalJson(int employeeId);
        Task<int> SetEmployeeClarificationForApproval(string userId, int approvalStatus, List<EmployeeClarificationApprovalViewModel> models);
        #endregion

        #region Attendance Report

        Task<JsonViewModel> DailyAttendanceReport(DateTime startDate, int companyId, int sbuId, int departmentId);
        Task<JsonViewModel> GetEmpWiseAttendanceReport(int companyId, string empId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> GetAttendanceSummaryByDateRange(int companyId, int sbuId, int departmentId, int empId, DateTime fromDate, DateTime toDate);

        #endregion


        #region Attendance Log Collection App
        Task<JsonViewModel> GetMaxVerifyDate(int machineNo);
        Task<JsonViewModel> GetAttendanceDeviceList();
        Task<bool> SetAttendanceLog(List<AttLog> model);

        #endregion

        Task<bool> SaveManualAttendance(string userId, ManualAttendanceViewModel model);
        Task<bool> SaveAttandanceClarification(string userId, AttandaceClarivicationViewModel model);
        Task<JsonViewModel> GetManualAttendance(int manualAttendanceId);

        #region JOIN HELDUP

        Task<JsonViewModel> HrmJoiningReportJson(int userId, DateTime date, int locationId, int departmentId);
        Task<JsonViewModel> HrmHeldupReportJson(int userId, DateTime date, int locationId, int departmentId);

        #endregion
    }
}
