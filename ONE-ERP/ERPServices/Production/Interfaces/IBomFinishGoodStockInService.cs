using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IBomFinishGoodStockInService
    {
        #region  Master

        Task<int> SaveBomFinishGoodStockInMaster(string userId, BomFinishGoodStockInMasterViewModel model);
        Task<bool> DeleteBomFinishGoodStockInMasterById(string userId, int bomStockInId);
        Task<JsonViewModel> GetBomFinishGoodStockInMasterById(int? bomStockInId);
        Task<JsonViewModel> GetMaxBomFinishGoodStockInNumber(DateTime date);
        Task<JsonViewModel> GetBomFinishGoodProductSpec(int? productId);

        #endregion

        #region Details

        Task<int> SaveBomFinishGoodStockInDetails(string userId, List<BomFinishGoodStockInDetailsViewModel> model, int bomStockInId);
        Task<JsonViewModel> GetBomFinishGoodStockInDetailsByMasterId(int? bomStockInId);
        Task<bool> DeleteBomFinishGoodStockInDetailsById(string userId, int BomFinishGoodStockInDetailsId);

        #endregion

        #region Reports

        Task<JsonViewModel> GetBomFinishGoodStockInReportDataById(int? bomStockInId);

        #endregion

        #region Create Auto Voucher  

        //Task<int> CreateAutoJournalForBom(string userId, BomViewModel model);

        #endregion
    }
}
