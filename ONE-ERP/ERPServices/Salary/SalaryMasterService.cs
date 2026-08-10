using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Salary.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Salary.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Salary
{
    public class SalaryMasterService : ISalaryMasterService
    {
        private readonly ERPDbContext _context;

        public SalaryMasterService(ERPDbContext context)
        {
            _context = context;
        }

        #region Salary Calulation Type
        public async Task<JsonViewModel> GetSalaryCalulationTypeById(int salaryCalulationTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetCalulationType {salaryCalulationTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Salary Type
        public async Task<JsonViewModel> GetSalaryTypeById(int salaryTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryType {salaryTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion       

        #region Salary Bonus Type
        public async Task<JsonViewModel> GetBonusTypeById(int bonusTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetBonusType {bonusTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Salary Wallet Type
        public async Task<bool> SaveWalletType(string userId, WalletTypeViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetWalletType {userId},{model.walletTypeId},{model.walletTypeName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetWalletTypeById(int walletTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetWalletType {walletTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateWalletType(int walletTypeId, string walletTypeName)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateWalletType {walletTypeId},{walletTypeName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteWalletTypeById(string userId, int walletTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteWalletType {userId},{walletTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Salary Head
        public async Task<bool> SaveSalaryHead(string userId, SalaryHeadViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetSalaryHead {userId},{model.salaryHeadId},{model.salaryHeadName},{model.headShortName},{model.salaryHeadCode},{model.salaryHeadType},{model.sortOrder},{model.isIncomeTax},{model.isInvestments},{model.isAdvance},{model.isArrear},{model.isBonus},{model.isMonthlyAllowance},{model.isLoan},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetSalaryHeadById(int salaryHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryHead {salaryHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<SalaryHeadViewModel>> GetSalaryHeadListById(int salaryHeadId)
        {
            var result = await _context.salaryHeadViewModels.FromSql($"SalarySpGetSalaryHeadList {salaryHeadId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateSalaryHead(int salaryHeadId, string salaryHeadName)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateSalaryHead {salaryHeadId},{salaryHeadName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalaryHeadById(string userId, int salaryHeadId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteSalaryHead {userId},{salaryHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Salary Grade
        public async Task<bool> SaveSalaryGrade(string userId, SalaryGradeViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetSalaryGrade {userId},{model.salaryGradeId},{model.gradeName},{model.payScale},{model.basicAmount},{model.currentBasic},{model.sortOrder},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetSalaryGradeById(int salaryGradeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryGrade {salaryGradeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateSalaryGrade(int salaryGradeId, string gradeName)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateSalaryGrade {salaryGradeId},{gradeName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalaryGradeById(string userId, int salaryGradeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteSalaryGrade {userId},{salaryGradeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Salary Slab
        public async Task<bool> SaveSalarySlab(string userId, SalarySlabViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetSalarySlab {userId},{model.salarySlabId},{model.salaryGradeId},{model.slabName},{model.slabAmount},{model.effectiveDate},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetSalarySlabById(int salarySlabId, int salaryGradeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalarySlab {salarySlabId},{salaryGradeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateSalarySlab(int salarySlabId, int salaryGradeId, string slabName)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateSalarySlab {salarySlabId},{salaryGradeId},{slabName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalarySlabById(string userId, int salarySlabId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteSalarySlab {userId},{salarySlabId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Salary Grade Percent
        public async Task<bool> SaveSalaryGradePercent(string userId, SalaryGradePercentViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetGradePercent {userId},{model.salaryGradePercentId},{model.salaryGradeId},{model.salaryHeadId},{model.salaryCalulationTypeId},{model.percentAmount},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetSalaryGradePercentById(int salaryGradePercentId, int salaryGradeId, int salaryHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetGradePercent {salaryGradePercentId},{salaryGradeId},{salaryHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<SalaryGradePercentViewModel>> GetSalaryGradePercentListById(int salaryGradePercentId, int salaryGradeId, int salaryHeadId)
        {
            var result = await _context.salaryGradePercentViewModels.FromSql($"SalarySpGetGradePercentList {salaryGradePercentId},{salaryGradeId},{salaryHeadId}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateSalaryGradePercent(int salaryGradePercentId, int salaryGradeId, int salaryHeadId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateGradePercent {salaryGradePercentId},{salaryGradeId},{salaryHeadId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalaryGradePercentById(string userId, int salaryGradePercentId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteGradePercent {userId},{salaryGradePercentId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Salary Period

        public async Task<bool> SaveSalaryPeriod(string userId, SalaryPeriodViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SalarySpSetSalaryPeriod {userId},{model.salaryPeriodId},{model.fiscalYearId},{model.salaryTypeId},{model.bonusTypeId},{model.periodName},{model.monthName},{model.lockStatus},{model.workingDays},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetSalaryPeriodById(int salaryPeriodId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryPeriod {salaryPeriodId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDuplicateSalaryPeriod(int salaryPeriodId, int fiscalYearId, int salaryTypeId, string monthName, string periodName)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetDuplicateSalaryPeriod {salaryPeriodId},{fiscalYearId},{salaryTypeId},{monthName},{periodName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalaryPeriodById(string userId, int salaryPeriodId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalarySpDeleteSalaryPeriod {userId},{salaryPeriodId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }



        #endregion
        public async Task<JsonViewModel> GetAllSalaryDepot(int? employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalarySpGetSalaryDepots {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
