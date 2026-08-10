
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface ILedgersService
    {
        Task<bool> SaveLedgers(string Id, LedgersViewModel ledgersViewModel);
        Task<IEnumerable<LedgersListViewModel>> GetLedgersList();
        Task<LedgersListViewModel> GetLedgersById(int id);
        Task<IEnumerable<LedgersListViewModel>> GetLedgersbyNatureId(int groupNatureId);
        Task<IEnumerable<LedgersListViewModel>> GetLedgersbyAccountGroupAccountNatureId(int accountGroupId, int groupNatureId);
        Task<JsonViewModel> GetLedgersByIdJson(int ledgerId, int accountGroupId, int natureId, int companyId, int sbuId, int ledgerTypeId);
        Task<JsonViewModel> GetDuplicateLedger(int ledgerId, string accountName);
        Task<bool> DeleteLedgersById(string Id, int ledgerId);
        Task<JsonViewModel> GetCOAJson(int? companyId = 0, int? groupNatureId = 0, int? accountGroupId = 0, int? accountSubGroupId = 0);
        Task<JsonViewModel> GetLedgersForVoucherCreate(int companyId, int sbuId);
        Task<JsonViewModel> GetAutoLedgerCode(int accountGroupId);
        Task<JsonViewModel> GetLedgersForVoucherCreateWithemp(int companyId, int sbuId, int empId);
        Task<JsonViewModel> GetLedgersByIdJsonwithemp(int ledgerId, int accountGroupId, int natureId, int companyId, int sbuId, int ledgerTypeId, int employeeId);
    }
}
