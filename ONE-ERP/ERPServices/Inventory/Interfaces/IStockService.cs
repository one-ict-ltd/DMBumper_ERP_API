using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IStockService
    {

        #region stock in

        Task<JsonViewModel> GetStockInById(int? stockMasterId);
        Task<JsonViewModel> GetStockDetailsInById(int? stockMasterId);
        Task<JsonViewModel> GetMaxMRNumber(DateTime MRDate);
        Task<int> SaveStockIn(string id, StockMasterViewModel stockInViewModel);
        Task<int> SaveStockInDetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);
        Task<bool> DeleteStockInById(string id, int stockMasterId);
        Task<bool> DeleteFactoryProductionStockIn(string id, int stockMasterId);
        Task<JsonViewModel> GetPurchaseOrderReceiveDetailsById(int? poReceiveId);
        #endregion

        #region STOCK IN WITH OUT PO
        Task<JsonViewModel> GetCurrentstockId(int? specificationId, int? storeId);
        Task<JsonViewModel> GetStockInWithOutPOById(int? userId, int? stockMasterId);
        Task<JsonViewModel> GetRmPmStockInWithOutPOById(int? userId, int? stockMasterId);
        Task<int> SaveStockInWithOutPO(string id, StockMasterViewModel stockInViewModel);
        Task<int> SaveStockInWithOutPO_FromTransferNote(string id, StockMasterViewModel stockInViewModel);
        Task<int> SaveRmPmStockInWithOutPO(string id, StockMasterViewModel stockInViewModel);
        Task<int> SaveStockInWithOutPODetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);
        Task<int> SaveStockInWithOutPODetails_FromTransferNote(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);
        Task<int> SaveRmPmStockInWithOutPODetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);

        Task<JsonViewModel> GetStockDetailsWithOutPOInById(int? stockMasterId);

        #endregion

        #region stock out

        Task<JsonViewModel> GetStockOutById(int? stockMasterId);

        Task<JsonViewModel> GetStockDetailsOutById(int? stockMasterId);
        Task<JsonViewModel> GetMaxSRNumber(DateTime SRDate);

        Task<int> SaveStockOut(string id, StockMasterViewModel stockInViewModel);

        Task<int> SaveStockOutDetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);
        Task<bool> DeleteStockOutById(string id, int stockMasterId);
        #endregion

        #region current stock report data ------------------
        Task<JsonViewModel> GetCurrentStockReport(int productId,int productWiseSpecificationId,int companyId, int sbuId ,int storeId,bool isStoreWiseGroup, int productTypeId);
        Task<JsonViewModel> GetAllStockWithoutBatch(int productId,int productWiseSpecificationId,int companyId, int sbuId ,int storeId,bool isStoreWiseGroup);
        #endregion

        #region Stock in report-------------------------
        Task<JsonViewModel> GetStockInReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup);
        Task<JsonViewModel> GetRepStockInReport(int stockMasterId);
        Task<JsonViewModel> GetRepStockInWithOutPoReport(int stockMasterId);
        #endregion

        #region Stock Out report-------------------------
        Task<JsonViewModel> GetStockOutReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup);
        Task<JsonViewModel> GetRepStockOutReport(int stockMasterId);

        #endregion

        #region stock transfer with TRN Report-------------
        Task<JsonViewModel> GetTRNNo(int fromsbuId, DateTime? fromDate, DateTime? toDate);
        Task<JsonViewModel> GetStockTransferReportData(DateTime? fromDate, DateTime? toDate, int? fromSbuId, int? fromStoreId,int? prodTrnfrId);
        #endregion

        #region stock receive------
        Task<JsonViewModel> GetProductTransferStockTRNById(int? stockReceiveSbuId);

        Task<JsonViewModel> GetMaxProductTransferNumber(DateTime dateTime);

        Task<JsonViewModel> GetStockReceiveById(int? userId, int stockReceiveId, string receiveType, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetProductTransferStockTRNByIdandType(int? stockReceiveSbuId, string type);
        Task<int> SaveStockReceive(string id, StockReceiveViewModel model);
        Task<int> SaveStockTransferReceiveDetails(string id, List<StockReceiveDetailsViewModel> detailsModel, int stockReceiveId);
        Task<bool> DeleteStockTransferReceiveById(int? userId, int masterId);
        Task<JsonViewModel> GetProductTransferStockDetailsById(int? prodReqId);
        Task<JsonViewModel> GetStockReceiveIdWiseInUpdate(int? stockReceiveId);

        Task<JsonViewModel> GetRptStockReceivePreview(int? stockReceiveId);

        Task<JsonViewModel> GetMaxStockTransferNumber(DateTime dateTime, int? userId, string receiveType);

        #endregion

        #region stock transfer receive -----------

        Task<JsonViewModel> getSRNo(int sbuId);
        Task<JsonViewModel> GetStockTransferReceiveReportData(int? sbuId, int? storeId, int? stockReceiveId,DateTime? fDate, DateTime? tDate);

        #endregion

        Task<int> SaveFactoryFGStockIn(string id, StockMasterViewModel stockInViewModel);
        Task<JsonViewModel> GetFactoryFGStockInJSON(int? userId, int? stockMasterId);
        Task<int> SaveFactoryFGStockInDetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);
        Task<JsonViewModel> GetFactoryFGProductionDetailsByIn(int? stockMasterId);
        Task<JsonViewModel> GetRptFactoryFGStockIn(int stockMasterId);
        Task<JsonViewModel> GetFactoryFGStockInJSONForStock(int? userId, int? stockMasterId);
        Task<JsonViewModel> GetStockInWithProductionById(int? userId, int? stockMasterId);
        Task<JsonViewModel> GetStockInWithProductionById_FromTransferNote(int? userId, int? stockMasterId, DateTime? fDate, DateTime? tDate);
        Task<int> UpdateFactoryFGStockIn(string id, StockMasterViewModel stockInViewModel);
        Task<int> SaveFactoryFGStockInDetailsQA(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId);
        Task<JsonViewModel> GetAllBatchFromStock();


        #region Miscellaneous Req. & Issue

        Task<JsonViewModel> GetMaterialTypeListForMiscellaneousReq(int? userId);
        Task<JsonViewModel> GetMiscellaneousIssueTypeList(int? userId);
        Task<JsonViewModel> GetPrdRmPmMiscellaneousReq(int? userId, int masterId, DateTime? fDate, DateTime? tDate);
        Task<int> SetPrdRmPmMiscellaneousReq(int? userId, PrdRmPmMiscellaneousReqViewModel model);
        Task<bool> DeletePrdRmPmMiscellaneousReq(int? userId, int masterId);
        Task<bool> DeletePrdRmPmMiscellaneousReqDetails(int? userId, int detailsId);

        //issue
        Task<JsonViewModel> GetPrdRmPmMiscellaneousIssue(int? userId, int masterId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetPrdRmPmMiscellaneousReqListForIssue(int? userId);
        Task<int> SetPrdRmPmMiscellaneousIssue(int? userId, PrdRmPmMiscellaneousIssueViewModel model);
        Task<bool> DeletePrdRmPmMiscellaneousIssue(int? userId, int masterId);

        #endregion Miscellaneous Req. & Issue

    }
}
