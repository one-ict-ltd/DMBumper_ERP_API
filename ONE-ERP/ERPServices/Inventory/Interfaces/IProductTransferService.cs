using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IProductTransferService
    {
        #region ProductTransferService Master

        Task<int> SaveProductTransfer(string id, ProductTransferViewModel model);
        Task<int> SaveProductTransferWithoutBatch(string id, ProductTransferViewModel model);
        Task<JsonViewModel> GetProductTransferById(int? userId, int? prodTrnfrId, string transferType, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetAllProductReqNumberBySbuId(int sbuId);
        Task<JsonViewModel> GetMaxProductTransferNumber(DateTime datetime, int? userId, string transferType);
        Task<bool> DeleteProductTransferById(string id, int prodTrnfrId);

        #endregion

        #region ProductTransferService Details
        Task<int> SaveProductTransferDetails(string id, List<ProductTransferDetailsViewModel> detailsModel, int purReqId);
        Task<int> SaveProductTransferDetailsWithoutBatch(string id, List<ProductTransferDetailsViewModel> detailsModel, int purReqId);
        Task<JsonViewModel> GetProductTransferDetailsByMasterId(int? purReqDetailsId);
        Task<JsonViewModel> GetProductReqDetailsForProdTrnsfrById(int? prodReqId, int? storeId);
        Task<bool> DeleteProductTrnfrDetailsById(string id, int productTrnfrDetailsId);
        Task<bool> DeleteProductTransferDetailsById(string id, int purReqDetailsId);
        Task<JsonViewModel> GetAllApprovedDestructionNoteNo(int? userId);
        Task<JsonViewModel> GetAllDestructionNoteReceive(int? userId);
        Task<JsonViewModel> GetDestructionNoteById(int? userId, int masterId);
        Task<bool> DeleteDestructionNoteReceiveById(int? userId, int masterId);

        Task<int> SaveDestructionNoteReceive(int? userId, DestructionNoteReceiveViewModel model);
        Task<int> SaveDestructionNoteDetails(int? userId, List<DestructionNoteReceiveDetailViewModel> detailsModel, int masterId);
        Task<JsonViewModel> GetDestructionNoteReceiveForRePack(int? userId);
        Task<JsonViewModel> GetDestructionNoteReceiveDetailForRePack(int? userId,int? destructionNoteReceiveId);
        Task<int> SaveRePackProductTransfer(int? userId, InvRePackProductTransferViewModel model);
        Task<int> SaveRePackProductTransferDetails(int? userId, List<InvRePackProductTransferDetailViewModel> lstDetailsViewModel, int masterId);
        #endregion

        Task<JsonViewModel> GetProductTransferDpottoDepotById(int? userId, int? prodTrnfrId);

        Task<JsonViewModel> GetRePackProductTransferById(int? userId, int? RePackProductTransferId);
        Task<JsonViewModel> GetRePackProductTransferNoListForReceive(int? userId, int? RePackProductTransferId);
        Task<JsonViewModel> GetRePackTransferDetailsById(int? userId, int? RePackProductTransferId);
        Task<bool> DeleteRePackProductTransferById(int? userId, int RePackProductTransferId);
        #region Reports
        Task<JsonViewModel> GetProductTransferReportData(int? userId, DateTime? fromDate, DateTime? toDate, int? fromSbuId, int? fromStoreId);
        #endregion
    }
}
