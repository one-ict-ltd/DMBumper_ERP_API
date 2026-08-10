using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Attendance.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.EmployeesInfo
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ERPDbContext _context;

        public AttendanceService(ERPDbContext context)
        {
            _context = context;
        }

        #region Calender

        public async Task<bool> SaveCalender(string userId, List<CalenderViewModel> calenderViewModels, CalenderViewModel model)
        {
            await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteCalenderByMonth {userId},{model.Year},{model.MonthNo}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateViewModel();
            foreach (CalenderViewModel calender in calenderViewModels)
            {
                result = await _context.saveUpdateViewModels.FromSql($"setCalender {userId},{calender.Day},{calender.Date},{calender.DayName},{model.MonthNo},{model.Year},{calender.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetCalender()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetCalender").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetCalenderByMonth(int year, int month)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetCalenderByMonth {year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCalenderByMonth(string userId, CalenderViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteCalenderByMonth {userId},{model.Year},{model.MonthNo}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region ShiftGroup Master

        public async Task<int> SaveShiftGroupMaster(string userId, ShiftGroupMasterViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AttnSpSetShiftGroupMaster {userId},{model.shiftMasterId},{model.shiftName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetShiftGroupMasterById(int shiftMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpGetShiftGroupMaster {shiftMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateShiftGroupMaster(int shiftMasterId, string shiftName)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpGetDuplicateShiftGroupMaster {shiftMasterId},{shiftName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteShiftGroupMasterById(string userId, int shiftMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AttnSpDeleteShiftGroupMaster {userId},{shiftMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region ShiftGroup Detail
        public async Task<int> SaveShiftGroupDetail(string userId, List<ShiftGroupDetailViewModel> shiftGroupDetailViewModels, int shiftMasterId)
        {
            await _context.saveUpdateViewModels.FromSql($"AttnSpDeleteShiftGroupDetail {userId},{shiftMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (ShiftGroupDetailViewModel model in shiftGroupDetailViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AttnSpSetShiftGroupDetail {userId},{shiftMasterId},{model.weekDay},{model.startTime},{model.endTime},{model.isHoliday}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetShiftGroupDetailByMasterId(int shiftMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpGetShiftGroupDetail {shiftMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion

        #region Assign Shift & Update PunchCard 

        public async Task<int> AssignShiftGroup(string userId, PunchCardViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AttnSpSetShiftGroupAssign {userId},{model.callName},{model.companyId},{model.sbuId},{model.department},{model.employeeId},{model.shiftMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetShiftAssignById(int punchCardId, int companyId, int sbuId, int employeeId, string department)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpGetShiftAssign {punchCardId},{companyId},{sbuId},{employeeId},{department}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPunchCardById(int punchCardId)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpGetPunchCard {punchCardId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeletePunchCardById(string userId, int punchCardId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AttnSpDeletePunchCard {userId},{punchCardId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> UpdatePunchCardNo(string userId, PunchCardViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AttnSpUpdatePunchCardNo {userId},{model.punchCardId},{model.punchCardNo}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Attendance Process

        public async Task<bool> ProcessAttendance(string userId, DateTime startDate, DateTime endDate, int companyId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"AttnSpProcessDailyAttendance {userId},{Convert.ToDateTime(startDate).ToString("yyyyMMdd")},{Convert.ToDateTime(endDate).ToString("yyyyMMdd")},{companyId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<JsonViewModel> GetAttendanceByDate(DateTime startDate, DateTime endDate, int companyId, int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AttnSpAttendanceReport {Convert.ToDateTime(startDate).ToString("yyyyMMdd")},{Convert.ToDateTime(endDate).ToString("yyyyMMdd")},{companyId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetEmployeeAttnClarificationById(int employeecClarificationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AttnSpGetEmployeeAttnClarificationById {employeecClarificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> SaveEmployeeAttnClarification(string userId, int empId, int employeecClarificationId, DateTime AttendanceDate, string clarification)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"AttnSpSetEmployeeAttnClarification {userId},{employeecClarificationId},{empId},{AttendanceDate},{clarification}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonViewModel> GetDuplicateAttendanceDateForClarification(int employeecClarificationId, DateTime? attendanceDate, int empId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpDuplicateAttendanceDateForClarification {employeecClarificationId},{attendanceDate},{empId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion


        #region Attendance Report

        public async Task<JsonViewModel> DailyAttendanceReport(DateTime startDate, int companyId, int sbuId, int departmentId)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpDailyAttendanceReport {startDate},{companyId},{sbuId},{departmentId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetEmpWiseAttendanceReport(int companyId, string empId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpEmpWiseAttendanceReport {companyId},{empId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAttendanceSummaryByDateRange(int companyId, int sbuId, int departmentId, int empId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AttnSpGetAttendanceSummaryByDateRangeJSON {companyId},{sbuId},{departmentId},{empId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SetEmployeeClarificationForApproval(string userId, int approvalStatus, List<EmployeeClarificationApprovalViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetEmployeeClarificationApproval {userId}, {model.employeecClarificationId}, {model.EmployeeClarificationLogId},{approvalStatus},{model.isSelect},{model.comments}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> HrmSpGetEmployeeClarificationForApprovalJson(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeClarificationJson {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion


        #region Attendance Log Collection App

        public async Task<JsonViewModel> GetMaxVerifyDate(int machineNo)
        {
            var result = await _context.jsonViewModels.FromSql($"sp_GetMaxVerifyDate {machineNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAttendanceDeviceList()
        {
            var result = await _context.jsonViewModels.FromSql($"sp_GetAttendanceDeviceList").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> SetAttendanceLog(List<AttLog> model)
        {
            var result = new SaveUpdateViewModel();
            foreach (var item in model)
            {
                result = await _context.saveUpdateViewModels.FromSql($"sp_SetAttendanceLog {item.User_ID}, {item.Verify_Date}, {item.Verify_State}, {item.Verify_Type}, {item.Work_Code}, {item.Device_ID}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }


        #endregion

        #region Manual Attendance 

        public async Task<bool> SaveManualAttendance(string userId, ManualAttendanceViewModel model)
        {
            var result = new SaveUpdateViewModel();
            result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetManualAttendance {userId},{model.manualAttendanceId},{model.employeeId},{model.startTime},{model.endTime},{model.remarks},{model.applicationDate},{model.workingTime}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> SaveAttandanceClarification(string userId, AttandaceClarivicationViewModel model)
        {
            try
            {
                var result = new SaveUpdateViewModel();
                result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetAttandanceClarification {userId},{model.attandanceClarificationId},{model.attandanceClarificationDate},{model.attandanceClarificationTime},{model.narration},{model.attandanceClarificationTypeId},{model.isApproved}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<JsonViewModel> GetManualAttendance(int manualAttendanceId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetManualAttendanceJson {manualAttendanceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region JOIN HELDUP

        public async Task<JsonViewModel> HrmJoiningReportJson(int userId, DateTime date, int locationId, int departmentId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetJoiningReportJSON {userId},{date},{locationId},{departmentId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> HrmHeldupReportJson(int userId, DateTime date, int locationId, int departmentId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryHeldupReportJSON {userId},{date},{locationId},{departmentId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

    }
}
