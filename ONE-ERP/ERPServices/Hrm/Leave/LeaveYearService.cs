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
    public class LeaveYearService: ILeaveYearService
    {
        private readonly ERPDbContext _context;

        public LeaveYearService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveLeaveYear(string Id, LeaveYearViewModel leaveYearViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetLeaveYear {Id},{leaveYearViewModel.leaveYearId},{leaveYearViewModel.yearName},{leaveYearViewModel.aliasName},{leaveYearViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<LeaveYearViewModel>> GetLeaveYear()
        {
            var result = await _context.leaveYearViewModels.FromSql($"HrmSpGetLeaveYear {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LeaveYearViewModel> GetLeaveYearById(int id)
        {
            var result = await _context.leaveYearViewModels.FromSql($"HrmSpGetLeaveYear {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeaveYearByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveYearJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateLeaveYear(int leaveYearId, string yearName)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateLeaveYear {leaveYearId},{yearName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteLeaveYearById(string Id, int leaveYearId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeaveYear {Id},{leaveYearId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
