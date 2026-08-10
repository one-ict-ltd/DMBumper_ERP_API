using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IPurchaseService
    {
        #region Purchase Master	
        Task<int> SavePurchase(string id, PurchaseViewModel model);
        Task<JsonViewModel> GetPurchaseById(int? purchaseOrderId);

        #endregion

        #region Purchase Details	
        Task<int> SavePurchaseDetails(string id, List<PurchaseDetailsViewModel> purchaseDetailsViewModels, int purchaseOrderId, decimal? totalVat, decimal? totalAit, decimal? freightCharge, decimal? grossAmount, int? storeId, bool? isAutoStock);

        #endregion
    }
}