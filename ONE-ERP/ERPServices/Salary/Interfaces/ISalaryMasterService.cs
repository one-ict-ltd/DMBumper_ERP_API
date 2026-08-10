using ONEERP.Areas.Salary.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Salary.Interfaces
{
    public interface ISalaryMasterService
    {
        #region Salary Calulation Type
        Task<JsonViewModel> GetSalaryCalulationTypeById(int salaryCalulationTypeId);
        #endregion

        #region Salary Type
        Task<JsonViewModel> GetSalaryTypeById(int salaryTypeId);
        #endregion

        #region Salary Bonus Type
        Task<JsonViewModel> GetBonusTypeById(int bonusTypeId);
        #endregion

        #region Salary Wallet Type
        Task<bool> SaveWalletType(string userId, WalletTypeViewModel model);
        Task<JsonViewModel> GetWalletTypeById(int walletTypeId);
        Task<JsonViewModel> GetDuplicateWalletType(int walletTypeId, string walletTypeName);
        Task<bool> DeleteWalletTypeById(string userId, int walletTypeId);
        #endregion

        #region Salary Head
        Task<bool> SaveSalaryHead(string userId, SalaryHeadViewModel model);
        Task<JsonViewModel> GetSalaryHeadById(int salaryHeadId);
        Task<IEnumerable<SalaryHeadViewModel>> GetSalaryHeadListById(int salaryHeadId);
        Task<JsonViewModel> GetDuplicateSalaryHead(int salaryHeadId, string salaryHeadName);
        Task<bool> DeleteSalaryHeadById(string userId, int salaryHeadId);
        #endregion

        #region Salary Grade
        Task<bool> SaveSalaryGrade(string userId, SalaryGradeViewModel model);
        Task<JsonViewModel> GetSalaryGradeById(int salaryGradeId);
        Task<JsonViewModel> GetDuplicateSalaryGrade(int salaryGradeId, string gradeName);
        Task<bool> DeleteSalaryGradeById(string userId, int salaryGradeId);
        #endregion

        #region Salary Slab
        Task<bool> SaveSalarySlab(string userId, SalarySlabViewModel model);
        Task<JsonViewModel> GetSalarySlabById(int salarySlabId, int salaryGradeId);
        Task<JsonViewModel> GetDuplicateSalarySlab(int salarySlabId, int salaryGradeId, string gradeName);
        Task<bool> DeleteSalarySlabById(string userId, int salarySlabId);
        #endregion

        #region Salary Grade Percent
        Task<bool> SaveSalaryGradePercent(string userId, SalaryGradePercentViewModel model);
        Task<JsonViewModel> GetSalaryGradePercentById(int salaryGradePercentId, int salaryGradeId, int salaryHeadId);
        Task<IEnumerable<SalaryGradePercentViewModel>> GetSalaryGradePercentListById(int salaryGradePercentId, int salaryGradeId, int salaryHeadId);
        Task<JsonViewModel> GetDuplicateSalaryGradePercent(int salaryGradePercentId, int salaryGradeId, int salaryHeadId);
        Task<bool> DeleteSalaryGradePercentById(string userId, int salaryGradePercentId);
        #endregion

        #region Salary Period

        Task<bool> SaveSalaryPeriod(string userId, SalaryPeriodViewModel model);
        Task<JsonViewModel> GetSalaryPeriodById(int salaryPeriodId);
        Task<JsonViewModel> GetDuplicateSalaryPeriod(int salaryPeriodId, int fiscalYearId, int salaryTypeId, string monthName, string periodName);
        Task<bool> DeleteSalaryPeriodById(string userId, int salaryPeriodId);
        #endregion
        Task<JsonViewModel> GetAllSalaryDepot(int? employeeId);

    }
}
