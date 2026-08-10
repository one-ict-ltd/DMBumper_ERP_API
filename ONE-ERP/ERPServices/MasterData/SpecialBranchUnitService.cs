using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class SpecialBranchUnitService: ISpecialBranchUnitService
    {
        private readonly ERPDbContext _context;

        public SpecialBranchUnitService(ERPDbContext context)
        {
            _context = context;
        }

        #region SBU
        public async Task<bool> SaveSpecialBranchUnit(string Id, SBUViewModel sbuViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetSBU {Id},{sbuViewModel.sbuId},{sbuViewModel.sbuName},{sbuViewModel.aliasName},{sbuViewModel.sbuCode},{sbuViewModel.shortOrder},{sbuViewModel.isDefault},{sbuViewModel.companyId},{sbuViewModel.isActive},{sbuViewModel.branchAddress}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<SBUListViewModel>> GetSpecialBranchUnit()
        {
            var result = await _context.sbuListViewModels.FromSql($"CmnSpGetSBU {0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<SBUListViewModel> GetSpecialBranchUnitById(int id)
        {
            var result = await _context.sbuListViewModels.FromSql($"CmnSpGetSBU {id},{0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SBUListViewModel>> GetSpecialBranchUnitBysbuidcompanyid(int id,int companyId)
        {
            var result = await _context.sbuListViewModels.FromSql($"CmnSpGetSBU {id},{companyId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidJson(int id, int companyId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSBUJson {id},{companyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidJson(int sbuId, int companyId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSBUJson {sbuId},{companyId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSpecialBranchUnitBySpecificSbuIdJson(int sbuId, int companyId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSbuBySbuIdJson {sbuId},{companyId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSpecialBranchUnitBycompanyidJson(int sbuId, int companyId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSBUALLJson {sbuId},{companyId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidForAccountingJson(int sbuId, int companyId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSBUForAccountingJson {sbuId},{companyId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSpecialBranchUnitBysbuidcompanyidForPurchaseRequisitionJson(int sbuId, int companyId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSBUForPurchaseRequisitionJson {sbuId},{companyId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSpecialBranchUnitBywithoutselfsbuidcompanyidJson(int sbuId, int companyId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSBUWithoutSelfJson {sbuId},{companyId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalaryLocationJson()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSalaryLocationJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteSpecialBranchUnitById(string Id, int SbuId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteSBU {Id},{SbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Store

        public async Task<bool> SaveStore(string id, StoreViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetStore {id},{model.storeId},{model.companyId},{model.sbuId},{model.storeName},{model.storeCode},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch( Exception ex)
            {
                throw ex;
            }
 
        }
       
        public async Task<JsonViewModel> GetStoreById(int employeeId,int companyId, int sbuId, int storeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetStoreJson {employeeId},{companyId},{sbuId},{storeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteStoreById(string id, int storeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStore {id},{storeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion
    }
}
