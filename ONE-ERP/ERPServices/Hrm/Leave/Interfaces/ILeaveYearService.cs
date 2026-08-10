using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave.Interfaces
{
   public interface ILeaveYearService
    {
        Task<bool> SaveLeaveYear(string Id, LeaveYearViewModel leaveYearViewModel);
        Task<IEnumerable<LeaveYearViewModel>> GetLeaveYear();
        Task<LeaveYearViewModel> GetLeaveYearById(int id);
        Task<JsonViewModel> GetLeaveYearByIdJson(int id);
        Task<JsonViewModel> GetDuplicateLeaveYear(int leaveYearId, string yearName);
        Task<bool> DeleteLeaveYearById(string Id, int leaveYearId);
    }
}
