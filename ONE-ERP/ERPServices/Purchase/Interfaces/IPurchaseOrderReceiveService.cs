using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IPurchaseOrderReceiveService
    {
        #region Purchase Order Recv. Master

        Task<int> SavePurchaseOrderReceive(string id, PurchaseOrderReceiveViewModel purReqViewModel);
        Task<JsonViewModel> GetPurchaseOrderReceiveById(int? poReceiveId);
        Task<bool> DeletePurchaseOrderReceiveById(string id, int poReceiveId);

        #endregion

        #region Purchase Order Recv. Details

        Task<int> SavePurchaseOrderReceiveDetails(string id, List<PurchaseOrderReceiveDetailsViewModel> purOrderReceiveDetailsViewModel, int poReceiveId);
        Task<JsonViewModel> GetPurchaseOrderReceiveDetailsByMasterId(int? poReceiveId);
        Task<JsonViewModel> GetPurchaseOrderDetailsByIdForPoRecv(int? purchaseOrderId);
        Task<bool> DeletePurchaseOrderReceiveDetailsById(string id, int poReceiveDetailsId);

        #endregion
    }
}
