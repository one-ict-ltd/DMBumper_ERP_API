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
    public class LeaveOpeningBalanceService: ILeaveOpeningBalanceService
    {
        private readonly ERPDbContext _context;

        public LeaveOpeningBalanceService(ERPDbContext context)
        {
            _context = context;
        } 
        public async Task<bool> SaveLeaveOpeningBalance(string Id, LeaveOpeningBalanceViewModel leaveOpeningBalanceViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetLeaveOpeningBlance {Id},{leaveOpeningBalanceViewModel.leaveOpeningBalanceId},{leaveOpeningBalanceViewModel.employeeId},{leaveOpeningBalanceViewModel.leaveTypeId},{leaveOpeningBalanceViewModel.yearId},{leaveOpeningBalanceViewModel.leaveDays},{leaveOpeningBalanceViewModel.leaveCarryDays},{leaveOpeningBalanceViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<LeaveOpeningBalanceViewModel>> GetLeaveOpeningBalance()
        {
            var result = await _context.leaveOpeningBalanceViewModels.FromSql($"HrmSpGetLeavePolicy{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LeaveOpeningBalanceViewModel> GetLeaveOpeningBalanceById(int id)
        {
            var result = await _context.leaveOpeningBalanceViewModels.FromSql($"HrmSpGetLeavePolicy {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeaveOpeningBalanceByIdJson(int id, int? employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveOpeningBalanceJson {id},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeaveOpeningBalanceByYearIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveOpeningBalanceByYearIdJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateLeaveOpeningBalance(int leaveOpeningBalanceId, int leaveYearId, int leaveTypeId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateLeaveOpeningBalance {leaveOpeningBalanceId},{leaveYearId},{leaveTypeId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteLeaveOpeningBalanceById(string Id, int leaveOpeningBalanceId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeaveOpeningBalance {Id},{leaveOpeningBalanceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
