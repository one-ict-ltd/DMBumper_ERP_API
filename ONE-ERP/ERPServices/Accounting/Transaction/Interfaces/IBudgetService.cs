using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface IBudgetService
    {
        #region Fiscal Year
        Task<bool> SaveFiscalYear(string id, FiscalYearViewModel fiscalYearViewModel);
        Task<JsonViewModel> GetFiscalYearById(int companyId, int sbuId, int fiscalYearId);
        Task<bool> DeleteFiscalYearById(string id, int fiscalYearId);
        Task<JsonViewModel> GetDuplicateFiscalYear(int fiscalYearId, string yearName);

        #endregion

        #region Budget Main Head
        Task<bool> SaveBudgetMainHead(string id, BudgetMainHeadViewModel budgetMainHeadViewModel);
        Task<JsonViewModel> GetBudgetMainHeadById(int companyId, int sbuId, int budgetMainHeadId);
        Task<bool> DeleteBudgetMainHeadById(string id, int budgetMainHeadId);

        #endregion

        #region Budget Sub Head
        Task<bool> SaveBudgetSubHead(string id, BudgetSubHeadViewModel budgetSubHeadViewModel);
        Task<JsonViewModel> GetBudgetSubHeadById(int budgetMainHeadId, int budgetSubHeadId);
        Task<bool> DeleteBudgetSubHeadById(string id, int budgetSubHeadId);

        #endregion

        #region Budget Head Master
        Task<int> SaveBudgetHeadMaster(string id, BudgetHeadMasterViewModel budgetHeadMasterViewModel);
        Task<JsonViewModel> GetBudgetHeadMasterById(int companyId, int sbuId, int budgetMainHeadId, int budgetSubHeadId, int budgetHeadMasterId);
        Task<bool> DeleteBudgetHeadMasterById(string id, int budgetHeadMasterId);

        #endregion

        #region Budget Head Details
        Task<int> SaveBudgetHeadDetails(string id, List<BudgetHeadDetailsViewModel> budgetHeadDetailsViewModels, int budgetHeadMasterId);
        Task<JsonViewModel> GetBudgetHeadDetailsByMasterId(int budgetHeadMasterId);
        Task<bool> DeleteBudgetHeadDetailsById(string id, int budgetHeadDetailsId);

        #endregion

        #region Budget Master
        Task<int> SaveBudgetMaster(string id, BudgetMasterViewModel budgetMasterViewModel);
        Task<JsonViewModel> GetBudgetMasterById(int companyId, int sbuId, int budgetMasterId);
        Task<bool> DeleteBudgetMasterById(string id, int budgetMasterId);       

        #endregion

        #region Budget Details
        Task<int> SaveBudgetDetails(string id, List<BudgetDetailsViewModel> budgetDetailsViewModels, int budgetMasterId);
        Task<JsonViewModel> GetBudgetDetailsByMasterId(int budgetMasterId);
        Task<bool> DeleteBudgetDetailsById(string id, int budgetDetailsId);

        #endregion      


    }
}
