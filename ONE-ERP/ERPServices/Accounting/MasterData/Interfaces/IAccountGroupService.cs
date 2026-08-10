
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IAccountGroupService
    {
        Task<bool> SaveAccountGroup(string Id, AccountGroupViewModel accountGroupViewModel);
        Task<IEnumerable<AccountGroupListViewModel>> GetAccountGroupList();
        Task<AccountGroupListViewModel> GetAccountGroupById(int id);
        Task<IEnumerable<AccountGroupListViewModel>> GetAccountGroupbyNatureId(int groupNatureId);
        Task<JsonViewModel> GetAccountGroupByIdNatureIdJson(int id, int groupNatureId);
        Task<JsonViewModel> GetParentChildAccountGroup(int groupNatureId);
        Task<JsonViewModel> GetDuplicateAccountGroup(int accountGroupId, string groupName);
        Task<JsonViewModel> GetAccountGroupSubGroup(int groupNatureId, int accountGroupId);
        Task<JsonViewModel> GetSubGroupByAccountGroupIds(int groupNatureId, string accountGroupId);
        Task<bool> DeleteAccountGroupById(string Id, int accountGroupId);
        Task<JsonViewModel> AccSpGetAccountLedgerAccessByUser(int groupNatureId, int accountGroupId, int employeeId);
        Task<JsonViewModel> AccSpGetAccountUserWiseLedger(int employeeId);
        Task<bool> SaveUserWiseLedger(string Id, UserWiseLedgerViewModel accountGroupViewModel);

    }
}
