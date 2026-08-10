
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface ITransactionModeService
    {
        Task<bool> SaveTransactionMode(string Id, TransactionModeViewModel transactionModeViewModel);
        Task<IEnumerable<TransactionModeListViewModel>> GetTransactionMode();
        Task<TransactionModeListViewModel> GetTransactionModeById(int id);
        Task<JsonViewModel> GetTransactionModeByIdJson(int id);
        Task<bool> DeleteTransactionModeById(string Id, int transactionModeId);
     
    }
}
