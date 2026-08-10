using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class LedgersService : ILedgersService
    {
        private readonly ERPDbContext _context;

        public LedgersService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveLedgers(string Id, LedgersViewModel ledgersViewModel)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetLedgers {Id},{ledgersViewModel.ledgerId},{ledgersViewModel.accountNatureId},{ledgersViewModel.accountGroupId},{ledgersViewModel.accountCode},{ledgersViewModel.accountName},{ledgersViewModel.aliasName},{ledgersViewModel.haveSubledger},{ledgersViewModel.currencyId},{ledgersViewModel.companyId},{ledgersViewModel.sbuId},{ledgersViewModel.isActive},{ledgersViewModel.ledgerTypeId},{ledgersViewModel.haveCostCentre},{ledgersViewModel.ledgerPrefix},{ledgersViewModel.noteId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<LedgersListViewModel>> GetLedgersList()
        {
            var result = await _context.ledgersListViewModels.FromSql($"AccSpGetLedgers {0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<LedgersListViewModel> GetLedgersById(int id)
        {
            var result = await _context.ledgersListViewModels.FromSql($"AccSpGetLedgers {id},{0},{0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLedgersByIdJson(int ledgerId, int accountGroupId, int natureId, int companyId, int sbuId, int ledgerTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetLedgersJson {ledgerId},{accountGroupId},{natureId},{companyId},{sbuId},{ledgerTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLedgersByIdJsonwithemp(int ledgerId, int accountGroupId, int natureId, int companyId, int sbuId, int ledgerTypeId,int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetLedgersJson {ledgerId},{accountGroupId},{natureId},{companyId},{sbuId},{ledgerTypeId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCOAJson(int? companyId = 0, int? groupNatureId = 0, int? accountGroupId = 0, int? accountSubGroupId = 0)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCOAJson_New {companyId}, {groupNatureId}, {accountGroupId}, {accountSubGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<LedgersListViewModel>> GetLedgersbyNatureId(int groupNatureId)
        {
            var result = await _context.ledgersListViewModels.FromSql($"AccSpGetLedgers {0},{0},{groupNatureId},{0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<LedgersListViewModel>> GetLedgersbyAccountGroupAccountNatureId(int accountGroupId, int groupNatureId)
        {
            var result = await _context.ledgersListViewModels.FromSql($"AccSpGetLedgers {0},{accountGroupId},{groupNatureId},{0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateLedger(int ledgerId, string accountName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateLedger {ledgerId},{accountName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteLedgersById(string Id, int ledgerId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteLedgers {Id},{ledgerId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetLedgersForVoucherCreate(int companyId, int sbuId)
        {
            //var result = await _context.jsonViewModels.FromSql($"AccSpGetLedgersForVoucher {companyId},{sbuId}").AsNoTracking().FirstOrDefaultAsync();
            //return result;

            var data = await _context.ledgersForVoucherViewModels.FromSql($"AccSpGetLedgersForVoucher {companyId},{sbuId}").AsNoTracking().ToListAsync();
            JsonViewModel result = new JsonViewModel();
            result.data = JsonSerializer.Serialize(data);
            return result;
        }

        public async Task<JsonViewModel> GetLedgersForVoucherCreateWithemp(int companyId, int sbuId, int empId)
        {
            var data = await _context.ledgersForVoucherViewModels.FromSql($"AccSpGetLedgersForVoucher {companyId},{sbuId},{empId}").AsNoTracking().ToListAsync();
            JsonViewModel result = new JsonViewModel();
            result.data = JsonSerializer.Serialize(data);
            return result;
        }

        public async Task<JsonViewModel> GetAutoLedgerCode(int accountGroupId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAutoLedgerCode {accountGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
