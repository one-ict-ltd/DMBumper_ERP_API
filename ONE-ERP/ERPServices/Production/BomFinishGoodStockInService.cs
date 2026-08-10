using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production
{
    public class BomFinishGoodStockInService : IBomFinishGoodStockInService
    {
        private readonly ERPDbContext _context;
        public BomFinishGoodStockInService(ERPDbContext context)
        {
            _context = context;
        }

        #region Master

        public async Task<int> SaveBomFinishGoodStockInMaster(string userId, BomFinishGoodStockInMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetBomFinishGoodStockInMaster {userId}, {model.bomStockInId}, {model.companyId}, {model.sbuId}, {model.storeId}, {model.stockInDate}, {model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }

        }
        public async Task<bool> DeleteBomFinishGoodStockInMasterById(string userId, int bomStockInId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteBomFinishGoodStockInMaster {userId}, {bomStockInId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetBomFinishGoodStockInMasterById(int? bomStockInId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomFinishGoodStockInMasterListJson {bomStockInId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxBomFinishGoodStockInNumber(DateTime date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxBomStockInNumberJson {date:yyyy-MMM-dd}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetBomFinishGoodProductSpec(int? productId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomFinishGoodProductSpecJSON {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Details

        public async Task<int> SaveBomFinishGoodStockInDetails(string userId, List<BomFinishGoodStockInDetailsViewModel> models, int bomStockInId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetBomFinishGoodStockInDetails {userId}, {model.bomStockInDetailsId}, {bomStockInId}, {model.bomId}, {model.qty}, {model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<bool> DeleteBomFinishGoodStockInDetailsById(string userId, int BomFinishGoodStockInDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteBomFinishGoodStockInDetails {userId}, {BomFinishGoodStockInDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetBomFinishGoodStockInDetailsByMasterId(int? bomStockInId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomFinishGoodStockInDetailsJson {bomStockInId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Reports

        public async Task<JsonViewModel> GetBomFinishGoodStockInReportDataById(int? bomStockInId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetBomReportJson {bomStockInId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Create Auto Voucher       

        //public async Task<int> CreateAutoJournalForBom(string userId, BomViewModel model)
        //{
        //    var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateBomJournal {userId},{model.grandTotal},{model.BomDate},{model.BomNo},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

        //    return result.isSuccess;
        //}

        #endregion
    }
}
