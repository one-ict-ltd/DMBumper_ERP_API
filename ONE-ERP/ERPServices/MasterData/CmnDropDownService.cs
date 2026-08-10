using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class CmnDropDownService : ICmnDropDownService
    {
        private readonly ERPDbContext _context;

        public CmnDropDownService(ERPDbContext context)
        {
            _context = context;
        }

        #region DropDown Type
        public async Task<bool> SaveCmnDropDownType(string id, CmnDropDownTypeViewModel cmnDropDownTypeViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetDropDownType {id},{cmnDropDownTypeViewModel.dropDownTypeId},{cmnDropDownTypeViewModel.dropDownType},{cmnDropDownTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
       
        public async Task<JsonViewModel> GetCmnDropDownTypeByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetDropDownType {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCmnDropDownTypeById(string id, int dropDownTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteDropDownType {id},{dropDownTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region DropDown

        public async Task<bool> SaveCmnDropDown(string id, CmnDropDownViewModel cmnDropDownViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetDropDown {id},{cmnDropDownViewModel.dropDownId},{cmnDropDownViewModel.dropDownTypeId},{cmnDropDownViewModel.dropDownValue},{cmnDropDownViewModel.dropDownText},{cmnDropDownViewModel.sortOrder},{cmnDropDownViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetCmnDropDownByIdJson(int id,string type)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetDropDown {id},{type}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetRegionByZoneCodes(int? userId,string zoneCodes)
        {
            if (string.IsNullOrWhiteSpace(zoneCodes) || zoneCodes.Contains("null"))
                zoneCodes = null;
            

            var result = await _context.jsonViewModels.FromSql($"SalSpGetRegionByZoneCodes {userId},{zoneCodes}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAreaByRegionCodes(int? userId,string regionCodes)
        {
            if (string.IsNullOrWhiteSpace(regionCodes) || regionCodes.Contains("null"))
                regionCodes = null;

            var result = await _context.jsonViewModels.FromSql($"SalSpGetAreaByRegionCodes {userId},{regionCodes}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTerritoryByAreaCodes(int? userId,string areaCodes)
        {
            if (string.IsNullOrWhiteSpace(areaCodes))
                areaCodes = null;

            var result = await _context.jsonViewModels.FromSql($"SalSpGetTerritoryByAreaCodes {userId},{areaCodes}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCmnDropDownById(string id, int dropDownId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteDropDown {id},{dropDownId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Bank

        public async Task<JsonViewModel> GetCmnBankByIdJson(int bankId,int bankTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBankById {bankId},{bankTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCmnOriginCountries(int countryId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetCountryOriginById {countryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion


        #region BankBranch

        public async Task<JsonViewModel> GetCmnBankBranchByBankIdJson(int bankId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBankBranchByBankId {bankId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Depot

        public async Task<JsonViewModel> GetCmnDepotByCodeJson(int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllDepot {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllTerritoriesHH(int userId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetAllTerritoriesHH").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region transaction Type

        public async Task<JsonViewModel> GetCmnTransactionType(int transactionTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetTransactionType {transactionTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion
    }
}
