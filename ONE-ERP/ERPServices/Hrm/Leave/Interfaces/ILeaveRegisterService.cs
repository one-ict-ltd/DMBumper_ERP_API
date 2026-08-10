using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave.Interfaces
{
   public interface ILeaveRegisterService
    {
        Task<JsonViewModel> GetLeaveBalance(int employeeId, int leaveYearId, int leaveTypeId);
        Task<bool> SaveLeaveRegister(string Id, LeaveRegisterViewModel leaveRegisterViewModel);
        Task<JsonViewModel> GetLeaveRegisterByemployeeIdJson(int id, int? empId);
        Task<JsonViewModel> GetLateClarificationByemployeeIdJson(int id, int? empId);
        Task<JsonViewModel> GetLeaveRegisterListByemployeeIdJson(DateTime fromDate, DateTime toDate,  int? employeeId, int id);
        Task<JsonViewModel> HrmSpGetLeaveRegisterForApprovalByEmployeeIdJson(int id);
        Task<JsonViewModel> GetLateAttandanceClarificationForApprovalByEmployeeIdJson(int id);
        Task<int> SetApproveLeave(string userId, int approvalStatus, List<LeaveRegisterViewModel> models);
        Task<int> SetApproveLateAttandance(int? userId, int? approvalStatus, HrmLateAttandaceVM model);
        Task<JsonViewModel> GetLeaveRegisterByIdJson(int id, int leaveId);

        Task<JsonViewModel> GetLeaveSummaryReportJson(int id, int year);
        Task<bool> DeleteLeaveRegisterById(string Id, int leaveRegisterId);
        Task<JsonViewModel> getDuplicateleaveRegister(int leaveRegisterId, DateTime? startDate, DateTime? endDate, int employeeId);
    }
}
