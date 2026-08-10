using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production
{
    public class ReagentIssueService : IReagentIssueService
    {
        private readonly ERPDbContext _context;
        public ReagentIssueService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<JsonViewModel> GetMaxReagentIssueNumber(DateTime reagentIssueDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxReagentIssueNo {reagentIssueDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetReagentRequisitionNumberforIssue(int userId)
        {

            try
            {
                var result = await _context.jsonViewModels.FromSql($"[dbo].[getReagentRequisitionNoForIssue] {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetReagentRequisitionByIdToIssue(int userId, int reagentReqId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"[dbo].[PrdSpGetReagentRequisitionDetailsToIssue] {userId}, {reagentReqId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveReagentIssueMaster(int userId, ReagentIssueViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveReagentIssueMaster {userId}, {model.reagentIssueMasterId}, {model.issueNo}, {model.issueDate}, {model.typeOfIssue}, {model.requisitionId}, {model.issueQty}, {model.issueStatus}, {model.issueRemarks}, {model.storeId},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveReagentIssueDetails(int userId, List<ProductionIssueDetailViewModel> lstDetailsViewModel, int issueId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var data in lstDetailsViewModel)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"[PrdSpSaveReagentIssueDetails] {userId}, {data.productIssueDetailId},{data.requisitinDetailId}, {issueId}, {data.qty}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetReagentIssueListByDate(int? userId, DateTime fromDate, DateTime toDate, int? issueId, string typeOfIssue)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetReagentIssueListByDate {fromDate},{toDate}, {issueId},{typeOfIssue},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetReagentIssueDetailsByMasterId(int? issueId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"[PrdSpGetReagentIssueDetailsByMasterId] {issueId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteReagentIssueById(string Id, int issueId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteReagentIssueById {Id}, {issueId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}
