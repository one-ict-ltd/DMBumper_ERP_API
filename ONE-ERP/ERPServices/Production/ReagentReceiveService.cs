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
    public class ReagentReceiveService : IReagentReceiveService
    {
        private readonly ERPDbContext _context;
        public ReagentReceiveService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<JsonViewModel> GetMaxReagentReceiveNumber(DateTime receiveDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxReagentReceiveNo {receiveDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetReagentIssueNumberForReceive(int userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"[dbo].[PrdSpGetReagentIssueNoForReceive] {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetReagentIssueDetailsByMasterIdForReceive(int? issueId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetReagentIssueDetailsDataForReceive {issueId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveReagentReceiveMaster(int userId, ReagentReceiveViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpGetSaveReagentReceiveMaster {userId}, {model.reagentReceiveMasterId}, {model.receiveNo}, {model.receiveDate}, {model.typeOfreceive}, {model.reagentIssueMasterId}, {model.receiveQty}, {model.receiveStatus}, {model.receiveRemarks},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveReagentReceiveDetails(string Id, List<ReagentReceiveDetailViewModel> model, int receiveId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var data in model)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveReagentReceiveDetail {Id}, {data.reagentReceiveDetailId},{data.reagentIssueDetailId}, {receiveId}, {data.qty},{data.potency},{data.grnNo}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        
        public async Task<JsonViewModel> GetReagentReceiveListByDate(int? userId, DateTime fromDate, DateTime toDate, int? receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetReagentReceiveDataByDate {fromDate},{toDate}, {receiveId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetReagentReceiveDetailsByMasterId(int? receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"[PrdSpGetReagentReceiveDetailsByMasterId] {receiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteReagentReceiveById(string Id, int receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteReagentReceiveById {Id}, {receiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
