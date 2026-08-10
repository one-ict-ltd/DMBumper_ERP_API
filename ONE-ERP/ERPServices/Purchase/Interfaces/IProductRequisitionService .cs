using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IProductRequisitionService
    {
        #region Prodct Req. Master

        Task<int> SaveProductReq(string id, ProductRequisitionViewModel purProductReqViewModel);
        Task<JsonViewModel> GetProductReqById(int? userId, int? prodReqId);//, int? productReqDetailsId);
        Task<bool> DeleteProductReqById(string id, int prodReqId);
        //Task<JsonViewModel> GetAllProductReq(int companyId, int sbuId, int? prodReqId);
        //Task<JsonViewModel> GetVoucherForCreateCheque(int companyId, int sbuId);
        Task<string> ValidateBatchWiseProductStock(int? userId, int? sbuId, int? productWiseSpecificationId, string batchNo, decimal? transferQty);

        Task<JsonViewModel> GetProductCurrentStockBySbuId(int productWiseSpecificationId, int sbuId);
        Task<int> SaveProductTransfer(string id, ProductRequisitionViewModel model);
        Task<int> SaveProductTransferDetails(string id, List<ProductReqDetailsViewModel> models, int prodTrnfrId);
        Task<JsonViewModel> getTerritoryOfficerByPartyId(int? partyId);


        #endregion

        #region Prodct Req. Details

        Task<int> SaveProductReqDetails(string id, List<ProductReqDetailsViewModel> purProductReqDetailsViewModel, int prodReqId);
        Task<JsonViewModel> GetProductReqDetailsById(int? productReqDetailsId);
        Task<bool> DeleteProductReqDetailsById(string id, int productReqDetailsId);

        #endregion

        #region Product Req Report-------------
        Task<JsonViewModel> GetRptGridProductReq(int? prodReqId);
        #endregion

        Task<JsonViewModel> GetProductApprvedRequisition(int UserId, int? approvedStatus, int? finalizeMasterId);
    }
}