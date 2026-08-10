
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface ILedgerTypeService
    {
        Task<bool> SaveLedgerType(string Id, LedgerTypeViewModel ledgerTypeViewModel);
        Task<IEnumerable<LedgerTypeListViewModel>> GetLedgerType();
        Task<LedgerTypeListViewModel> GetLedgerTypeById(int id);
        Task<JsonViewModel> GetLedgerTypeByIdJson(int id);
        Task<bool> DeleteLedgerTypeById(string Id, int ledgerTypeId);
     
    }
}
