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
    public class LeaveTypeService: ILeaveTypeService
    {
        private readonly ERPDbContext _context;

        public LeaveTypeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveLeaveType(string Id, LeaveTypeViewModel leaveTypeViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetLeaveType {Id},{leaveTypeViewModel.leaveTypeId},{leaveTypeViewModel.typeName},{leaveTypeViewModel.aliasName},{leaveTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<LeaveTypeViewModel>> GetLeaveType()
        {
            var result = await _context.leaveTypeViewModels.FromSql($"HrmSpGetLeaveType {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LeaveTypeViewModel> GetLeaveTypeById(int id)
        {
            var result = await _context.leaveTypeViewModels.FromSql($"HrmSpGetLeaveType {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeaveTypeByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveTypeJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateLeaveType(int leaveTypeId, string leaveTypeName)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateLeaveType {leaveTypeId},{leaveTypeName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteLeaveTypeById(string Id, int currencyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeaveType {Id},{currencyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
