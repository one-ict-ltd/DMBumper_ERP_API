using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IPurchaseRequisitionService
    {
        #region Purchase Req. Master

        Task<int> SavePurchaseReq(string id, PurchaseRequisitionViewModel purReqViewModel);
        //Task<int> SavePurchaseFianlReq(string id, RequisitionFinalMasterViewModel purFianlReqMasterViewModel);
        Task<JsonViewModel> GetPurchaseReqById(int? userId, int? prodReqId, int? isHo);
        Task<JsonViewModel> IsPurchaseRequisitionFinalisedByPRId(int? prodReqId);
        Task<bool> DeletePurchaseReqById(string id, int purReqId);

        #endregion

        #region Comparative Statement
        Task<int> SaveComparativeStatement(string id, ComparativeStatementMasterViewModel purFianlReqMasterViewModel);
        Task<JsonViewModel> GetComparativeStatementById(int? userId,int? comparativeStatementMasterId);
        Task<bool> DeleteComparativeStatementById(string id, int csMasterId);

        Task<JsonViewModel> GetCSListForApproval(string userId, int purchaseReqId,int approvalStatus);
        Task<int> UpdateCSMasterStatus(string userId, int? approvalStatus, List<ComparativeStatementDetailViewModel> models);

        Task<JsonViewModel> GetAllComparativeStatementsbyStatus(int approvalStatus,int quotationTypeId);
        Task<JsonViewModel> GetAllComparativeStatementsForLCbyStatus(int approvalStatus,int quotationTypeId);
        Task<JsonViewModel> GetCSDetailsbyMasterId(int? csMasterId, int? supplierId);
        #endregion


        #region Final Purchase Req
        Task<int> SavePurchaseFianlReq(string id, RequisitionFinalMasterViewModel purFianlReqMasterViewModel);
       
        Task<JsonViewModel> GetPurchaseFinalReqById(int? userId, int? finalRequisitionId);
        Task<JsonViewModel> isFinalisedRequisitionWordOrderedByFRId(int? finalRequisitionId);
        Task<JsonViewModel> GetPurchaseFinalReqDetailByMasterIdForPdfReport(int? finalRequisitionId);
        Task<JsonViewModel> GetAllFinalizedRequisitions(int? finalRequisitionId, int appStatus);
        Task<JsonViewModel> GetAllFinalizeRequisitionDetailByMasterId(int? finalRequisitionId, int? supplierId);
        Task<bool> DeleteFinalPurchaseReqById(string id, int finalRequisitionId);

        Task<int> SavePurchaseFianlReqDetails(string id, List<RequisitionFinalMasterDetailViewmodel> purFinalReqDetailsViewModel, int purFinalReqId);

        #endregion


        #region Purchase Req. Details
        Task<int> SavePurchaseReqDetails(string id, List<PurchaseReqDetailsViewModel> purReqDetailsViewModel, int purReqId);

      
        //Task<int> SavePurchaseFianlReqDetails(string id, List<RequisitionFinalMasterDetailViewmodel> purFinalReqDetailsViewModel, int purFinalReqId);
        Task<int> SaveComparativeStatementDetails(string id, List<ComparativeStatementDetailViewModel> compartativeStatementDetails, int purCSFinalReqId);

        Task<JsonViewModel> GetPurchaseReqDetailsById(int? purReqDetailsId);
    
        Task<JsonViewModel> GetPurchaseReqDetailsByMasterId(int? masterIddd);

        Task<bool> DeletePurchaseReqDetailsById(string id, int purReqDetailsId);
        Task<JsonViewModel> getRequisitionRevision();

        #endregion

        #region Purchase Req. Approval
        Task<int> ApprovePurchaseReqMaster(string userId, string approvalStatus, List<PurchaseReqDetailsViewModel> models);
        Task<int> UpdatePurchaseReqDetails(string userId, List<PurchaseReqDetailsViewModel> models);
        Task<JsonViewModel> GetPurchaseReqMasterListForApproval(string userId, int purchaseReqId);
        Task<JsonViewModel> GetPurchaseReqDetailsByIdForApproval(int purchaseReqId);
        Task<JsonViewModel> GetPurchaseReqMasterListByStatus(string userId, int status);
        #endregion

        #region purchase requisition---------
        Task<JsonViewModel> GetPurchaseRequisitionGridReport(int? prodReqId);
        #endregion


        #region Quotation Collection

        Task<int> SaveQuotationCollection(string id, QuotationCollectionViewModel model);
        Task<JsonViewModel> GetQuotationCollectionById(int? userId, int? quotationCollectionId);
        Task<bool> DeleteQuotationCollectionById(string id, int quotationCollectionId);

        #endregion

        #region  Quotation Collection Details
        Task<int> SaveQuotationCollDetails(string id, List<QuotationCollectionDetailsViewModel> dataList, int quotationCollectionId);
        Task<JsonViewModel> GetQuotationCollDetailsById(int? purReqDetailsId);

        Task<JsonViewModel> GetQuotationCollDetailsByMasterId(int? masterIddd);

        Task<bool> DeleteQuotationCollDetailsById(string id, int purReqDetailsId);

        #endregion

        #region requisition Approval Matrix
        Task<int> SavePurchaseApprovalMatrix(string empid, List<PurchaseApprovalMatrixViewModel> leaveApprovalMatrixViewModels, int? employeeId, int? deptId, int? productTypeId);
        Task<JsonViewModel> GetPurchaseApprovalMatrix(int? id, int? empId, int? productTypeId);
        Task<bool> DeletePurchaseApprovalMatrixByTypeId(string id, int? employeeId, int? productTypeId);
        #endregion requisition Approval Matrix
    }
}
