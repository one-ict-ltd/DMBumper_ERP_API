using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Hrm.Leave.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Hrm.Leave
{
    public class LeaveApprovalMatrixService: ILeaveApprovalMatrixService
    {
        private readonly ERPDbContext _context;

        public LeaveApprovalMatrixService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetLeaveApprovalMatrixByEmployeeIdJson(int id,int? empId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveApprovalMatrixByemployeeIdJson {id},{empId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveApprovalMatrix(string empid, List<LeaveApprovalMatrixViewModel> leaveApprovalMatrixViewModels, int employeeId,int deptId)
        {
            await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeaveApprovalMatrix {empid},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (LeaveApprovalMatrixViewModel model in leaveApprovalMatrixViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetLeaveApprovalMatrix {empid},{model.leaveApprovalMatrixId},{employeeId},{model.approverId},{model.isFinalApproval},{model.seqNo},{model.isActive},{deptId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<bool> DeleteApprovalMatrixByTypeId(string id, int employeeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeaveApprovalMatrix {id},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

    }
}
