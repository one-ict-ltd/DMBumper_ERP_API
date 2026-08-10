using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class CostCentreService : ICostCentreService
    {
        private readonly ERPDbContext _context;

        public CostCentreService(ERPDbContext context)
        {
            _context = context;
        }

        #region  CostCentre
        public async Task<bool> SaveCostCentre(string Id, CostCentreViewModel costCentreViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetCostCentre {Id},{costCentreViewModel.costCentreId},{costCentreViewModel.costCentreName},{costCentreViewModel.aliasName},{costCentreViewModel.AccCostCenterCategoryId},{costCentreViewModel.AccCostCenterLocationId},{costCentreViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<CostCentreListViewModel>> GetCostCentreList()
        {
            var result = await _context.costCentreListViewModels.FromSql($"AccSpGetCostCentre {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<CostCentreListViewModel> GetCostCentreById(int id)
        {
            var result = await _context.costCentreListViewModels.FromSql($"AccSpGetCostCentre {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCostCentreByIdJson(int costCentreId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostCentreJson {costCentreId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateCostCentre(int costCentreId, string costCentreName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCostCentre {costCentreId},{costCentreName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetCostCentrebyCategoryIdandLocationId(int costCostCentreLocationId, int costCentreCategoryId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostCentrebyCategoryIdandLocationId {costCostCentreLocationId},{costCentreCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCostCentreById(string Id, int costCentreId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostCentre {Id},{costCentreId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  CostCentre Mapping

        public async Task<bool> SaveCostCentreBranchMapping(string Id, CostCentreBranchMappingViewModel mappingViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetCostCentreBranchMapping {Id},{mappingViewModel.costCentreMappingId},{mappingViewModel.costCentreId},{mappingViewModel.companyId},{mappingViewModel.sbuId},{mappingViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetCostCentreBranchMappingByIdJson(int costCentreMappingId, int costCentreId, int companyId, int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostCentreBranchMappingJson {costCentreMappingId},{costCentreId},{companyId},{sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateCostCentreMapping(int costCentreMappingId, int costCentreId, int companyId, int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCostCentreMapping {costCentreMappingId},{costCentreId},{companyId},{sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCostCentreBranchMappingById(string Id, int costCentreMappingId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostCentreBranchMapping {Id},{costCentreMappingId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Formula Type
        public async Task<JsonViewModel> GetFormulaType(int formulaTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetFormulaType {formulaTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Cost Sheet Parent Head

        public async Task<JsonViewModel> GetCostSheetParentHead(int parentHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostSheetParentHead {parentHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region  Cost Sheet Head

        public async Task<int> SaveCostSheetHead(string userId, CostSheetHeadViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetCostSheetHead {userId},{model.costSheetHeadId},{model.parentHeadId},{model.costHeadName},{model.description},{model.sortOrder},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
       
        public async Task<JsonViewModel> GetCostSheetHeadById(int costSheetHeadId, int parentHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostSheetHead {costSheetHeadId},{parentHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateCostSheetHead(int costSheetHeadId, string costHeadName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCostSheetHead {costSheetHeadId},{costHeadName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCostSheetHeadById(string userId, int costSheetHeadId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostSheetHead {userId},{costSheetHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Cost Sheet Head Amount/Balance
        public async Task<int> SaveCostSheetHeadAmount(string userId, List<CostSheetHeadAmountViewModel> costSheetHeadAmountViewModels, int costSheetHeadId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostSheetHeadAmount {userId},{costSheetHeadId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (CostSheetHeadAmountViewModel model in costSheetHeadAmountViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetCostSheetHeadAmount {userId},{model.costSheetHeadAmountId},{costSheetHeadId},{model.formulaTypeId},{model.ledgerId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetCostSheetHeadAmountById(int costSheetHeadAmountId, int costSheetHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostSheetHeadAmount {costSheetHeadAmountId},{costSheetHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        

        public async Task<JsonViewModel> GetDuplicateCostSheetHeadAmount(int costSheetHeadAmountId, int costSheetHeadId, int ledgerId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCostSheetHeadAmount {costSheetHeadAmountId},{costSheetHeadId},{ledgerId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCostSheetHeadAmountById(string userId, int costSheetHeadId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostSheetHeadAmount {userId},{costSheetHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Cost Report
        public async Task<JsonViewModel> RptCostSheet(DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpRptCostSheet {Convert.ToDateTime(fromDate).ToString("yyyyMMdd")},{Convert.ToDateTime(toDate).ToString("yyyyMMdd")}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region cost center category

        public async Task<bool> SaveCostCentreCategory(string Id, CostCentreCategoryViewModel costCentreViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetCostCentreCategory {Id},{costCentreViewModel.costCentreCategoryId},{costCentreViewModel.costCentreCategoryName},{costCentreViewModel.costCentreCategoryCode},{costCentreViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetCostCentreCategoryByIdJson(int costCostCentreCategoryId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostCentreCategoryJson {costCostCentreCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCostCentreCategoryById(string Id, int costCostCentreCategoryId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostCentreCategory {Id},{costCostCentreCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDuplicateCostCentreCategory(int costCostCentreCategoryId, string costCentreCategoryName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCostCentreCategory {costCostCentreCategoryId},{costCentreCategoryName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region cost center Location

        public async Task<bool> SaveCostCentreLocation(string Id, CostCentreLocationViewModel costCentreViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetCostCentreLocation {Id},{costCentreViewModel.costCentreLocationId},{costCentreViewModel.costCentreLocationName},{costCentreViewModel.costCentreLocationCode},{costCentreViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetCostCentreLocationByIdJson(int costCostCentreLocationId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCostCentreLocationJson {costCostCentreLocationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCostCentreLocationById(string Id, int costCostCentreLocationId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostCentreLocation {Id},{costCostCentreLocationId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDuplicateCostCentreLocation(int costCostCentreLocationId, string costCentreLocationName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCostCentreLocation {costCostCentreLocationId},{costCentreLocationName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion
    }
}
