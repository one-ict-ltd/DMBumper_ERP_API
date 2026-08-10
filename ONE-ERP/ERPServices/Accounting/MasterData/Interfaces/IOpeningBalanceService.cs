
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface IOpeningBalanceService
    {
        Task<bool> SaveOpeningBalance(string Id, OpeningBalanceViewModel openingBalanceViewModel);
        Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceList();
        Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceListbyLedgerId(int ledgerId);
        Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceListbyLedgerIdPartyId(int ledgerId, int partyId);
        Task<IEnumerable<OpeningBalanceListViewModel>> GetOpeningBalanceListbyPartyId(int partyId);
        Task<OpeningBalanceListViewModel> GetOpeningBalanceListbyId(int openingBalanceId);
        Task<JsonViewModel> GetOpeningBalanceListbyIdJson(int openingBalanceId, int ledgerId, int partyId);
        Task<JsonViewModel> GetDuplicateOpeningBalance(int openingBalanceId, int ledgerId, int? partyId);
        Task<bool> DeleteOpeningBalanceById(string Id, int openingBalanceId);
    }
}
