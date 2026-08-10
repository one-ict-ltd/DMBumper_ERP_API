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
    public class LeaveRegisterService: ILeaveRegisterService
    {
        private readonly ERPDbContext _context;

        public LeaveRegisterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetLeaveBalance(int employeeId, int leaveYearId, int leaveTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmLeaveBalance {employeeId},{leaveYearId},{leaveTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> SaveLeaveRegister(string Id, LeaveRegisterViewModel leaveRegisterViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetLeaveRegister {Id},{leaveRegisterViewModel.leaveRegisterId},{leaveRegisterViewModel.employeeId},{leaveRegisterViewModel.substituteEmployeeId},{leaveRegisterViewModel.leaveTypeId},{leaveRegisterViewModel.yearId},{leaveRegisterViewModel.leaveDay},{leaveRegisterViewModel.type},{leaveRegisterViewModel.remarks},{leaveRegisterViewModel.leaveLocation},{leaveRegisterViewModel.startDate},{leaveRegisterViewModel.endDate},{leaveRegisterViewModel.isActive},{leaveRegisterViewModel.emergencyContact}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
           
        }

        public async Task<JsonViewModel> GetLeaveRegisterByemployeeIdJson(int id,int? empId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveRegisterByEmployeeIdJson {id},{empId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLateClarificationByemployeeIdJson(int id,int? empId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLateClarificationByEmployeeIdJson {id},{empId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLeaveRegisterListByemployeeIdJson(DateTime fromDate, DateTime toDate, int? employeeId, int id)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveRegisterListByEmployeeIdJson {fromDate},{toDate}, {employeeId},{id}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetLeaveSummaryReportJson(int id, int year)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmLeaveSummaryReportJson {id},{year}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetLeaveRegisterByIdJson(int id, int leaveId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveRegisterByIdJson {id},{leaveId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> HrmSpGetLeaveRegisterForApprovalByEmployeeIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLeaveRegisterForApprovalByEmployeeIdJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLateAttandanceClarificationForApprovalByEmployeeIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetLateForApproval {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SetApproveLeave (string userId, int approvalStatus, List<LeaveRegisterViewModel> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetLeaveAproval_new {userId}, {model.leaveRegisterId}, {model.leaveApprovalLogId},{approvalStatus},{model.isSelect},{model.comments},{model.leaveTypeId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public async Task<int> SetApproveLateAttandance(int? userId, int? approvalStatus, HrmLateAttandaceVM model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var item in model.lstMasterViewModel)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetLateAttandanceAproval {userId}, {item.attandanceClarificationId}, {item.lateAttandanceApprovalLog},{approvalStatus},{item.isSelect},{item.comments},{1}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<bool> DeleteLeaveRegisterById(string Id, int leaveRegisterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteLeaveRegister {Id},{leaveRegisterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> getDuplicateleaveRegister(int leaveRegisterId, DateTime? startDate, DateTime? endDate, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetDuplicateLeaveRegister {leaveRegisterId},{startDate},{endDate},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

    }
}
