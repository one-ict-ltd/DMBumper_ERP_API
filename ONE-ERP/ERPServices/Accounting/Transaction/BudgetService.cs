using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class BudgetService : IBudgetService
    {
        private readonly ERPDbContext _context;

        public BudgetService(ERPDbContext context)
        {
            _context = context;
        }

        #region Fiscal Year
        public async Task<bool> SaveFiscalYear(string id, FiscalYearViewModel fiscalYearViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetFiscalYear {id},{fiscalYearViewModel.fiscalYearId},{fiscalYearViewModel.companyId},{fiscalYearViewModel.sbuId},{fiscalYearViewModel.yearName},{fiscalYearViewModel.yearStartDate},{fiscalYearViewModel.yearEndDate},{fiscalYearViewModel.lockDate},{fiscalYearViewModel.isActive},{fiscalYearViewModel.financialYearName},{fiscalYearViewModel.islocked}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetFiscalYearById(int companyId, int sbuId, int fiscalYearId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetFiscalYearJson {companyId},{sbuId},{fiscalYearId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteFiscalYearById(string id, int fiscalYearId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteFiscalYear {id},{fiscalYearId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDuplicateFiscalYear(int fiscalYearId, string yearName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateFiscalYear {fiscalYearId},{yearName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }        

        #endregion

        #region Budget Main Head
        public async Task<bool> SaveBudgetMainHead(string id, BudgetMainHeadViewModel budgetMainHeadViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetBudgetMainHead {id},{budgetMainHeadViewModel.budgetMainHeadId},{budgetMainHeadViewModel.companyId},{budgetMainHeadViewModel.sbuId},{budgetMainHeadViewModel.mainHeadCode},{budgetMainHeadViewModel.mainHeadName},{budgetMainHeadViewModel.sortOrder},{budgetMainHeadViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBudgetMainHeadById(int companyId, int sbuId, int budgetMainHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBudgetMainHeadJson {companyId},{sbuId},{budgetMainHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteBudgetMainHeadById(string id, int budgetMainHeadId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetMainHead {id},{budgetMainHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Budget Sub Head
        public async Task<bool> SaveBudgetSubHead(string id, BudgetSubHeadViewModel budgetSubHeadViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetBudgetSubHead {id},{budgetSubHeadViewModel.budgetSubHeadId},{budgetSubHeadViewModel.budgetMainHeadId},{budgetSubHeadViewModel.subHeadCode},{budgetSubHeadViewModel.subHeadName},{budgetSubHeadViewModel.sortOrder},{budgetSubHeadViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBudgetSubHeadById(int budgetMainHeadId, int budgetSubHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBudgetSubHeadJson {budgetMainHeadId},{budgetSubHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteBudgetSubHeadById(string id, int budgetSubHeadId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetSubHead {id},{budgetSubHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Budget Head Master
        public async Task<int> SaveBudgetHeadMaster(string id, BudgetHeadMasterViewModel budgetHeadMasterViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetBudgetHeadMaster {id},{budgetHeadMasterViewModel.budgetHeadMasterId},{budgetHeadMasterViewModel.budgetMainHeadId},{budgetHeadMasterViewModel.budgetSubHeadId},{budgetHeadMasterViewModel.headCode},{budgetHeadMasterViewModel.headName},{budgetHeadMasterViewModel.sortOrder},{budgetHeadMasterViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBudgetHeadMasterById(int companyId, int sbuId, int budgetMainHeadId, int budgetSubHeadId, int budgetHeadMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBudgetHeadMasterJson {companyId},{sbuId},{budgetMainHeadId},{budgetSubHeadId},{budgetHeadMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteBudgetHeadMasterById(string id, int budgetHeadMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetHeadMaster {id},{budgetHeadMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Budget Head Details
        public async Task<int> SaveBudgetHeadDetails(string id, List<BudgetHeadDetailsViewModel> budgetHeadDetailsViewModels, int budgetHeadMasterId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetHeadDetails {id},{budgetHeadMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (BudgetHeadDetailsViewModel budgetHeadDetailsViewModel in budgetHeadDetailsViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetBudgetHeadDetails {id},{0},{budgetHeadMasterId},{budgetHeadDetailsViewModel.ledgerId},{budgetHeadDetailsViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBudgetHeadDetailsByMasterId(int budgetHeadMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBudgetHeadDetailsJson {budgetHeadMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteBudgetHeadDetailsById(string id, int budgetHeadDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetHeadDetails {id},{0},{budgetHeadDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Budget Master
        public async Task<int> SaveBudgetMaster(string id, BudgetMasterViewModel budgetMasterViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetBudgetMaster {id},{budgetMasterViewModel.budgetMasterId},{budgetMasterViewModel.fiscalYearId},{budgetMasterViewModel.companyId},{budgetMasterViewModel.sbuId},{budgetMasterViewModel.budgetNo},{budgetMasterViewModel.budgetDate},{budgetMasterViewModel.grandTotal},{budgetMasterViewModel.status},{budgetMasterViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBudgetMasterById(int companyId, int sbuId, int budgetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBudgetMasterJson {companyId},{sbuId},{budgetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteBudgetMasterById(string id, int budgetMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetMaster {id},{budgetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Budget Details

        public async Task<int> SaveBudgetDetails(string id, List<BudgetDetailsViewModel> budgetDetailsViewModels, int budgetMasterId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteBudgetDetails {id},{budgetMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (BudgetDetailsViewModel budgetDetailsViewModel in budgetDetailsViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetBudgetDetails {id},{0},{budgetMasterId},{budgetDetailsViewModel.budgetHeadMasterId},{budgetDetailsViewModel.firstMonth},{budgetDetailsViewModel.secondMonth},{budgetDetailsViewModel.thirdMonth},{budgetDetailsViewModel.fourthMonth},{budgetDetailsViewModel.fifthMonth},{budgetDetailsViewModel.sixthMonth},{budgetDetailsViewModel.seventhMonth},{budgetDetailsViewModel.eighthMonth},{budgetDetailsViewModel.ninethMonth},{budgetDetailsViewModel.tenthMonth},{budgetDetailsViewModel.eleventhMonth},{budgetDetailsViewModel.twelvethMonth},{budgetDetailsViewModel.subTotal},{budgetDetailsViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBudgetDetailsByMasterId(int budgetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBudgetDetailsJson {budgetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteBudgetDetailsById(string id, int budgetDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteChequeBookDetails {id},{0},{budgetDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion
    }
}
