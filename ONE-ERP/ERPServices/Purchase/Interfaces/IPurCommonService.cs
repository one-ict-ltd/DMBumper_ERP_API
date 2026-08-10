using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IPurCommonService
    {
        //Task<JsonViewModel> GetAllUsers(string ProductReqNumber);        
        Task<JsonViewModel> GetProductReqNumber(string id, string prodReqNumber);
        Task<JsonViewModel> GetPurchaseReqNumber(string id, string purReqNumber);
        Task<JsonViewModel> GetPurchaseOrderNumber(string id, string purOrderNumber);
        Task<JsonViewModel> GetPurchaseOrderReceiveNumber(string id, string purOrderReceiveNumber);
        Task<JsonViewModel> GetMaxPurchaseFinalReqNumber(DateTime purchaseFinalReqDate);
        Task<JsonViewModel> GetMaxComperativeStatementNo(DateTime productdate);
        Task<JsonViewModel> getQuotationCollectionNoName();
        Task<JsonViewModel> getQuotationCollectionDetail(int masterId, int csMasterId);
        Task<JsonViewModel> GetMaxProductReqNumber(int userId, DateTime prodReqDate);
        Task<JsonViewModel> GetMaxPurchaseReqNumber(DateTime purchaseReqDate);
        Task<JsonViewModel> GetMaxPurchaseOrderNumber(DateTime purchaseOrderDate);
        Task<JsonViewModel> GetMaxPurchaseOrderReceiveNumber(DateTime purchaseOrderRecvDate);

        Task<JsonViewModel> GetMaxImportShipmentNumber(DateTime todayDate);
        Task<JsonViewModel> GetMaxImportPreLcRequisitionNumber(DateTime purchaseReqDate);
        Task<JsonViewModel> GetMaxImportLcNumber(DateTime purchaseReqDate);
        #region Qu
        Task<JsonViewModel> GetMaxQuotationCollectionNumber(DateTime quotationCollDate);
        #endregion
        Task<JsonViewModel> GetMaxGRNNo(DateTime grnDate);
        Task<JsonViewModel> GetMaxGRNImpNo(DateTime grnDate);
        Task<JsonViewModel> GetMaxPlanNo(DateTime planDate);
        Task<JsonViewModel> GetMaxBatchNo(DateTime planDate);
        Task<JsonViewModel> GetMaxBillNo(DateTime billDate);
        Task<JsonViewModel> GetMaxBillPaymentNo(DateTime paymentDate);

    }
}