using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IPurchaseReturnService
    {
        #region Purchase Return Master
        Task<int> SavePurchaseReturnMaster(string id, PurchaseReturnMasterViewModel model);
        Task<JsonViewModel> GetPurchaseReturnMasterByMasterId(int? purchaseReturnMasterId);
        Task<bool> DeletePurchaseReturnMasterByMasterId(string id, int purchaseReturnMasterId);
        Task<JsonViewModel> GetMaxPurchaseReturnNumber(DateTime datetime);
        Task<JsonViewModel> GetPOListBySupplierId(int? supplierId);

        #endregion

        #region Purchase Return Details

        Task<int> SavePurchaseReturnDetails(string id, List<PurchaseReturnDetailsViewModel> purchaseReturnDetailsViewModels, int purchaseReturnMasterId);
        Task<JsonViewModel> GetPurchaseReturnDetailsByMasterId(int purchaseReturnMasterId);

        #endregion

    }
}