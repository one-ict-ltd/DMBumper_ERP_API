using ONEERP.Areas.Hrm.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave.Interfaces
{
    public interface ILeaveApprovalMatrixService
    {
        Task<JsonViewModel> GetLeaveApprovalMatrixByEmployeeIdJson(int id, int? empId);
        Task<int> SaveApprovalMatrix(string empid, List<LeaveApprovalMatrixViewModel> leaveApprovalMatrixViewModels, int employeeId, int deptId);
        Task<bool> DeleteApprovalMatrixByTypeId(string id, int employeeId);
    }
}
