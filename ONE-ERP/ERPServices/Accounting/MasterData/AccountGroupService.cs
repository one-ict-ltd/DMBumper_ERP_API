using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class AccountGroupService : IAccountGroupService
    {
        private readonly ERPDbContext _context;

        public AccountGroupService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAccountGroup(string Id, AccountGroupViewModel accountGroupViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetAccountGroup {Id},{accountGroupViewModel.accountGroupId},{accountGroupViewModel.parentId},{accountGroupViewModel.groupNatureId},{accountGroupViewModel.groupCode},{accountGroupViewModel.groupName},{accountGroupViewModel.isActive},{accountGroupViewModel.sortOrder}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> SaveUserWiseLedger(string Id, UserWiseLedgerViewModel accountGroupViewModel)
        {
            for (int i = 0; i < accountGroupViewModel.lstModel.Count(); i++)
            {
                if (accountGroupViewModel.lstModel[i].isActive == true)
                {
                    await _context.saveUpdateViewModels.FromSql($"AccSpSetAccountUserWiseLedger {Id},{accountGroupViewModel.employeeId},{accountGroupViewModel.lstModel[i].ledgerId}").AsNoTracking().FirstOrDefaultAsync();
                }
                else
                {
                    await _context.saveUpdateViewModels.FromSql($"AccSpDeleteAccountUserWiseLedger {accountGroupViewModel.employeeId},{accountGroupViewModel.lstModel[i].ledgerId}").AsNoTracking().FirstOrDefaultAsync();
                }
            } 
            return true;
        }
        public async Task<IEnumerable<AccountGroupListViewModel>> GetAccountGroupList()
        {
            var result = await _context.accountGroupListViewModels.FromSql($"AccSpGetAccountGroup {0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<AccountGroupListViewModel> GetAccountGroupById(int id)
        {
            var result = await _context.accountGroupListViewModels.FromSql($"AccSpGetAccountGroup {id},{0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<AccountGroupListViewModel>> GetAccountGroupbyNatureId(int groupNatureId)
        {
            var result = await _context.accountGroupListViewModels.FromSql($"AccSpGetAccountGroup {0},{groupNatureId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAccountGroupByIdNatureIdJson(int id, int groupNatureId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAccountGroupJson {id},{groupNatureId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetParentChildAccountGroup(int groupNatureId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetParentChildAccountGroupJson {groupNatureId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateAccountGroup(int accountGroupId, string groupName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateAccountGroup {accountGroupId},{groupName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> GetAccountGroupSubGroup(int groupNatureId, int accountGroupId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAccountGroupSubGroup {groupNatureId},{accountGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }        
        public async Task<JsonViewModel> GetSubGroupByAccountGroupIds(int groupNatureId, string accountGroupId)
        {
            //var STR = $"AccSpGetSubGroupByAccountGroupIds {groupNatureId},{accountGroupId}";
            var result = await _context.jsonViewModels.FromSql($"AccSpGetSubGroupByAccountGroupIds {groupNatureId},{accountGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> AccSpGetAccountLedgerAccessByUser(int groupNatureId, int accountGroupId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAccountLedgerAccessByUser {groupNatureId},{accountGroupId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> AccSpGetAccountUserWiseLedger(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAccountUserWiseLedger {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteAccountGroupById(string Id, int accountGroupId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteAccountGroup {Id},{accountGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
