using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface ICmnSettingService
    {
        #region Approval Type    

        Task<JsonViewModel> GetMenuWiseTransactionDateUnlockList(int? id, int masterId);
        Task<JsonViewModel> GetMenuListForTransactionDateUnlock(int? id);
        Task<int> SaveMenuWiseTransactionDateUnlock(int? id, MenuWiseTransactionDateUnlockViewModel model);
        Task<int> DeleteMenuWiseTransactionDateUnlock(int? id, int masterId);

        #endregion
    }
}
