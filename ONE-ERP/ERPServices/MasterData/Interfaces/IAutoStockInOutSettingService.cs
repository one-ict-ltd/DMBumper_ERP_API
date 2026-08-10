using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface IAutoStockInOutSettingService
    {
        #region Approval Type    
        
        Task<JsonViewModel> GetAutoStockInOutSettingStatusById(int id);

        #endregion
    }
}
