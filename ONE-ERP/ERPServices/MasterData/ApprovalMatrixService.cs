using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class ApprovalMatrixService : IApprovalMatrixService
    {
        private readonly ERPDbContext _context;

        public ApprovalMatrixService(ERPDbContext context)
        {
            _context = context;
        }

        #region Approval Type           

        public async Task<JsonViewModel> GetApprovalTypeById(int approvalTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetApprovalType {approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<int> SaveApprovalType(string Id, ApprovalTypeViewModel approvalTypeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetApprovalType {Id},{approvalTypeViewModel.approvalTypeId},{approvalTypeViewModel.approvalTypeName},{approvalTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteApprovalTypeByTypeId(string id, int approvalTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteaApprovalTypeId {id},{approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Approver Type           

        public async Task<JsonViewModel> GetApproverTypeById(int approverTypeId, int approvalTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetApproverType {approverTypeId},{approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetApproverType(int approverTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAllApproverType {approverTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveApproverType(string Id, ApproverTypeViewModel approveTypeViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetApproverType {Id},{approveTypeViewModel.approverTypeId},{approveTypeViewModel.approvalTypeId},{approveTypeViewModel.approverTypeName},{approveTypeViewModel.employeeId},{approveTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteApproverTypeId(string id, int approverTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteaApproverTypeId {id},{approverTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Approval Matrix

        public async Task<int> SaveApprovalMatrix(string empid, List<ApprovalMatrixViewModel> approvalMatrixViewModels,int approvalTypeId)
        {
            await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteApprovalMatrix {empid},{approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (ApprovalMatrixViewModel model in approvalMatrixViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetApprovalMatrix {empid},{model.approvalTypeId},{model.approverTypeId},{model.companyId},{model.sbuId},{model.nextApproverId},{model.sequenceNo},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetApprovalMatrix(int approvalTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetApprovalMatrix {approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetApprovalMatrixByTypeId(int approvalTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetApprovalMatrixByType {approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteApprovalMatrixByTypeId(string id, int approvalTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteApprovalMatrix {id},{approvalTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion
    }
}
