using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<bool> SaveLeaveType(string Id, LeaveTypeViewModel currencyViewModel);
        Task<IEnumerable<LeaveTypeViewModel>> GetLeaveType();
        Task<LeaveTypeViewModel> GetLeaveTypeById(int id);
        Task<JsonViewModel> GetLeaveTypeByIdJson(int id);
        Task<JsonViewModel> GetDuplicateLeaveType(int currencyId, string currencyName);
        Task<bool> DeleteLeaveTypeById(string Id, int currencyId);
    }
}
