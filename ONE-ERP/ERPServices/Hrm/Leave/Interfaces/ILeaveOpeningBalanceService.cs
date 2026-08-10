using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave.Interfaces
{
   public interface ILeaveOpeningBalanceService
    {
        Task<bool> SaveLeaveOpeningBalance(string Id, LeaveOpeningBalanceViewModel leavePolicyViewModel);
        Task<IEnumerable<LeaveOpeningBalanceViewModel>> GetLeaveOpeningBalance();
        Task<LeaveOpeningBalanceViewModel> GetLeaveOpeningBalanceById(int id);
        Task<JsonViewModel> GetLeaveOpeningBalanceByIdJson(int id,int? employeeId);
        Task<JsonViewModel> GetDuplicateLeaveOpeningBalance(int leaveOpeningBalanceId, int leaveYearId, int leaveTypeId,int employeeId);
        Task<bool> DeleteLeaveOpeningBalanceById(string Id, int leaveOpeningBalanceId);
        Task<JsonViewModel> GetLeaveOpeningBalanceByYearIdJson(int id);
    }
}
