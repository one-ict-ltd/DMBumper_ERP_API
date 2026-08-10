using Microsoft.EntityFrameworkCore;
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
    public class LeavePolicyService: ILeavePolicyService
    {
        private readonly ERPDbContext _context;

        public LeavePolicyService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveLeavePolicy(string Id, LeavePolicyViewModel leavePolicyViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetLeavePolicy {Id},{leavePolicyViewModel.leavePolicyId},{leavePolicyViewModel.leaveTypeId},{leavePolicyViewModel.yearId},{leavePolicyViewModel.yearlyMaxLeave},{leavePolicyViewModel.yearlyMaxCarry},{leavePolicyViewModel.remarks},{leavePolicyViewModel.weeklyOffBridge},{leavePolicyViewModel.govtHolidayBridge},{leavePolicyViewModel.paymentType},{leavePolicyViewModel.highestCarryForward},{leavePolicyViewModel.maxBridgeLimit},{leavePolicyViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<LeavePolicyViewModel>> GetLeavePolicy()
        {
            var result = await _context.leavePolicyViewModels.FromSql($"HrmSpGetLeavePolicy{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LeavePolicyViewModel> GetLeavePolicyById(int id)
        {
            var result = await _context.leavePolicyViewModels.FromSql($"HrmSpGetLeavePolicy {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeavePolicyByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeavePolicyJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeavePolicyByYearIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeavePolicyByYearIdJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateLeavePolicy(int leavePolicyId, int leaveYearId, int leaveTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateLeavePolicy {leaveYearId},{leaveTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteLeavePolicyById(string Id, int leavePolicyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeavePolicy {Id},{leavePolicyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> ProcessLeavePolicyById(string Id, int leavePolicyId,int yearId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpProcessLeavePolicy {Id},{leavePolicyId},{yearId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
