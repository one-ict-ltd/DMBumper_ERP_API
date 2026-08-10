
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface ICostCentreService
    {

        #region  CostCentre
        Task<bool> SaveCostCentre(string Id, CostCentreViewModel costCentreViewModel);
        Task<IEnumerable<CostCentreListViewModel>> GetCostCentreList();
        Task<CostCentreListViewModel> GetCostCentreById(int id);       
        Task<JsonViewModel> GetCostCentreByIdJson(int costCentreId);
        Task<JsonViewModel> GetDuplicateCostCentre(int costCentreId, string costCentreName);
        Task<bool> DeleteCostCentreById(string Id, int costCentreId);

        #endregion

        #region  CostCentre Mapping

        Task<bool> SaveCostCentreBranchMapping(string Id, CostCentreBranchMappingViewModel mappingViewModel);
        Task<JsonViewModel> GetCostCentreBranchMappingByIdJson(int costCentreMappingId, int costCentreId, int companyId, int sbuId);
        Task<JsonViewModel> GetDuplicateCostCentreMapping(int costCentreMappingId, int costCentreId, int companyId, int sbuId);
        Task<bool> DeleteCostCentreBranchMappingById(string Id, int costCentreMappingId);

        #endregion

        #region Formula Type
        Task<JsonViewModel> GetFormulaType(int formulaTypeId);

        #endregion

        #region Cost Sheet Parent Head
        Task<JsonViewModel> GetCostSheetParentHead(int parentHeadId);

        #endregion

        #region  Cost Sheet Head
        Task<int> SaveCostSheetHead(string userId, CostSheetHeadViewModel model);       
        Task<JsonViewModel> GetCostSheetHeadById(int costSheetHeadId, int parentHeadId);
        Task<JsonViewModel> GetDuplicateCostSheetHead(int costSheetHeadId, string costHeadName);
        Task<bool> DeleteCostSheetHeadById(string userId, int costSheetHeadId);

        #endregion

        #region  Cost Sheet Head Amount/Balance
        Task<int> SaveCostSheetHeadAmount(string userId, List<CostSheetHeadAmountViewModel> costSheetHeadAmountViewModels, int costSheetHeadId);
        Task<JsonViewModel> GetCostSheetHeadAmountById(int costSheetHeadAmountId, int costSheetHeadId);        
        Task<JsonViewModel> GetDuplicateCostSheetHeadAmount(int costSheetHeadAmountId, int costSheetHeadId, int ledgerId);
        Task<bool> DeleteCostSheetHeadAmountById(string userId, int costSheetHeadId);

        #endregion

        #region Cost Report
        Task<JsonViewModel> RptCostSheet(DateTime fromDate, DateTime toDate);


        #endregion

        #region Cost center Category
        Task<bool> SaveCostCentreCategory(string Id, CostCentreCategoryViewModel costCentreViewModel);
        Task<JsonViewModel> GetCostCentreCategoryByIdJson(int costCostCentreCategoryId);
        Task<bool> DeleteCostCentreCategoryById(string Id, int costCostCentreCategoryId);
        Task<JsonViewModel> GetDuplicateCostCentreCategory(int costCostCentreCategoryId, string costCentreCategoryName);
        #endregion

        #region Cost center Location
        Task<bool> SaveCostCentreLocation(string Id, CostCentreLocationViewModel costCentreViewModel);
        Task<JsonViewModel> GetCostCentreLocationByIdJson(int costCostCentreLocationId);
        Task<bool> DeleteCostCentreLocationById(string Id, int costCostCentreLocationId);
        Task<JsonViewModel> GetDuplicateCostCentreLocation(int costCostCentreLocationId, string costCentreLocationName);
        Task<JsonViewModel> GetCostCentrebyCategoryIdandLocationId(int costCostCentreLocationId, int costCentreCategoryId);
        #endregion
    }
}
