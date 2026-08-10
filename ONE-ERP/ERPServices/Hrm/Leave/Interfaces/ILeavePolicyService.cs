using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave.Interfaces
{
   public interface ILeavePolicyService
    {
        Task<bool> SaveLeavePolicy(string Id, LeavePolicyViewModel leavePolicyViewModel);
        Task<IEnumerable<LeavePolicyViewModel>> GetLeavePolicy();
        Task<LeavePolicyViewModel> GetLeavePolicyById(int id);
        Task<JsonViewModel> GetLeavePolicyByIdJson(int id);
        Task<JsonViewModel> GetDuplicateLeavePolicy(int leavePolicyId,int leaveYearId, int leaveTypeId);
        Task<bool> DeleteLeavePolicyById(string Id, int leavePolicyId);
        Task<bool> ProcessLeavePolicyById(string Id, int leavePolicyId, int yearId);
        Task<JsonViewModel> GetLeavePolicyByYearIdJson(int id);
    }
}
