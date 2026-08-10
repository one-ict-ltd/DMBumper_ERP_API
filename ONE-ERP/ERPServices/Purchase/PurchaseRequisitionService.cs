using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class PurchaseRequisitionService : IPurchaseRequisitionService
    {
        private readonly ERPDbContext _context;
        public PurchaseRequisitionService(ERPDbContext context)
        {
            _context = context;
        }

        #region Prodct Req. Master

        public async Task<bool> DeletePurchaseReqById(string id, int purchaseReqId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseRequisition {id}, {purchaseReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseReqById(int? userId, int? purchaseReqId, int? isHo)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqInfoJSON {userId},{purchaseReqId},{isHo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SavePurchaseReq(string id, PurchaseRequisitionViewModel ReqViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseRequisition {id}, {ReqViewModel.purchaseReqId}, {ReqViewModel.productReqId},{ReqViewModel.purchaseReqDate},{ReqViewModel.fromWarehouseId},{ReqViewModel.toWarehouseId},{ReqViewModel.approvalStatus},{ReqViewModel.purpose},{ReqViewModel.isUrgency},{ReqViewModel.isActive}, {ReqViewModel.isHO},{ReqViewModel.productTypeId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> IsPurchaseRequisitionFinalisedByPRId(int? purchaseReqId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpIsPurchaseRequisitionFinalisedByPRId {purchaseReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Comparative Statement

        public async Task<int> SaveComparativeStatement(string id, ComparativeStatementMasterViewModel purFianlReqMasterViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveComparativeStatement {id},{purFianlReqMasterViewModel.csMasterNo},{purFianlReqMasterViewModel.csDate},{purFianlReqMasterViewModel.quotationCollectionMasterId},{purFianlReqMasterViewModel.remarks},{purFianlReqMasterViewModel.csMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetComparativeStatementById(int? userId, int? comparativeStatementMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetComparativeStatementJSON {userId},{comparativeStatementMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;

        }

        public async Task<JsonViewModel> GetAllComparativeStatementsbyStatus(int approvalStatus, int quotationTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetAllComparativeStatementsJSON {approvalStatus},{quotationTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllComparativeStatementsForLCbyStatus(int approvalStatus, int quotationTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetAllComparativeStatementsForLCJSON {approvalStatus},{quotationTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetCSDetailsbyMasterId(int? csMasterId, int? supplierId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetCSDetailsbyMasterId {csMasterId},{supplierId}").AsNoTracking().FirstOrDefaultAsync();
            return result;

        }
        public async Task<bool> DeleteComparativeStatementById(string id, int csMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteComparativeStatementById {id}, {csMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }


        public async Task<int> SaveComparativeStatementDetails(string id, List<ComparativeStatementDetailViewModel> compartativeStatementDetails, int cSMasterId)
        {
            try
            {
                var isDelete = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteCSDetailsByCSMasterId {id}, {cSMasterId}").AsNoTracking().FirstOrDefaultAsync();

                var result = new SaveUpdateValueViewModel();

                foreach (ComparativeStatementDetailViewModel model in compartativeStatementDetails)
                {
                    if (model.isSelect == true)
                    {

                        result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetCSDetails {id},{model.CsDetailId},{cSMasterId},{model.partyId},{model.approvedqty},{model.rate},{model.rateFrom},{model.quotationCollectionDetailId},{model.manufactureOrigin},{model.BudgetCreateId},{model.Discount}").AsNoTracking().FirstOrDefaultAsync();

                    }


                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
                // return 0;
            }
        }

        public async Task<JsonViewModel> GetCSListForApproval(string userId, int csId, int approvalStatus)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetCSApprovalJson {userId}, {csId}, {approvalStatus}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> UpdateCSMasterStatus(string userId, int? approvalStatus, List<ComparativeStatementDetailViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetCSMasterMasterApproval {userId}, {model.csMasterId},{approvalStatus},{model.isSelect},{model.comments}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        #endregion

        #region Final Purchase Req. Details
        public async Task<int> SavePurchaseFianlReq(string id, RequisitionFinalMasterViewModel purFianlReqMasterViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveFinalReq {id}, {purFianlReqMasterViewModel.requisitionFinalizeMasterId},{purFianlReqMasterViewModel.finalRequsitionNo}, {purFianlReqMasterViewModel.requisitionFianlDate},{purFianlReqMasterViewModel.remarks}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetPurchaseFinalReqById(int? userId, int? finalRequisitionId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseFianlReqInfoJSON {userId},{finalRequisitionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> isFinalisedRequisitionWordOrderedByFRId(int? finalRequisitionId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpIsFinalisedRequisitionWordOrderedByFRId {finalRequisitionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteFinalPurchaseReqById(string id, int finalRequisitionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteFinalPurchaseRequisition {id}, {finalRequisitionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SavePurchaseFianlReqDetails(string id, List<RequisitionFinalMasterDetailViewmodel> RequisitionFinalMasterDetailViewmodel, int purFinalReqId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (RequisitionFinalMasterDetailViewmodel purfinalReqDetailsViewModel in RequisitionFinalMasterDetailViewmodel)
                {
                    //var x = $"PurSpSetPurchaseFianlReqDetails {id},{purfinalReqDetailsViewModel.requisitionFinalizeDetailId},{purFinalReqId},{purfinalReqDetailsViewModel.PurchaseReqDetailsId},{purfinalReqDetailsViewModel.isCS},{purfinalReqDetailsViewModel.PartyId},{purfinalReqDetailsViewModel.finalQty},{purfinalReqDetailsViewModel.rate}";

                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseFianlReqDetails {id},{purfinalReqDetailsViewModel.requisitionFinalizeDetailId},{purFinalReqId},{purfinalReqDetailsViewModel.PurchaseReqDetailsId},{purfinalReqDetailsViewModel.isCS},{purfinalReqDetailsViewModel.PartyId},{purfinalReqDetailsViewModel.finalQty},{purfinalReqDetailsViewModel.rate},{purfinalReqDetailsViewModel.vatAmount},{purfinalReqDetailsViewModel.vatPercentage},{purfinalReqDetailsViewModel.BudgetCreateId},{purfinalReqDetailsViewModel.prodSpecification}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<JsonViewModel> GetAllFinalizedRequisitions(int? finalRequisitionId, int appStatus)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetAllFinalizedRequisitionsJSON {finalRequisitionId},{appStatus}").AsNoTracking().FirstOrDefaultAsync();
            return result; throw new NotImplementedException();
        }

        public async Task<JsonViewModel> GetAllFinalizeRequisitionDetailByMasterId(int? finalRequisitionId, int? supplierId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetAllFinalizeRequisitionDetailByMasterIdJSON {finalRequisitionId},{supplierId}").AsNoTracking().FirstOrDefaultAsync();
            return result; throw new NotImplementedException();
        }

        #endregion

        #region Purchase Req. Details



        public async Task<bool> DeletePurchaseReqDetailsById(string id, int purReqDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseReqDetails {id}, {purReqDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseReqDetailsById(int? purReqDetailsId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqDetailsJSON {purReqDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetPurchaseReqDetailsByMasterId(int? masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqDetailsByMasterId {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SavePurchaseReqDetails(string id, List<PurchaseReqDetailsViewModel> purPurchaseReqDetailsViewModels, int purchaseReqId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (PurchaseReqDetailsViewModel purReqDetailsViewModel in purPurchaseReqDetailsViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseReqDetails {id},{purReqDetailsViewModel.purchaseReqDetailsId},{purchaseReqId},{purReqDetailsViewModel.productReqDetailsId},{purReqDetailsViewModel.productId},{purReqDetailsViewModel.productWiseSpecificationId},{purReqDetailsViewModel.reqQty},{purReqDetailsViewModel.price},{purReqDetailsViewModel.isActive},{purReqDetailsViewModel.prodSpecification},{purReqDetailsViewModel.purchaseOrderDetailId},{purReqDetailsViewModel.receivedQty},{purReqDetailsViewModel.currentStockQty},{purReqDetailsViewModel.vatAmount},{purReqDetailsViewModel.revisionId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> getRequisitionRevision()
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetRequisitionRevision").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion

        #region  Purchase Req. Approval 
        public async Task<int> ApprovePurchaseReqMaster(string userId, string approvalStatus, List<PurchaseReqDetailsViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseReqMasterMasterAproval {userId}, {model.purchaseReqId},{approvalStatus},{model.isSelect},{model.comments},{model.approvalLogId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<int> UpdatePurchaseReqDetails(string userId, List<PurchaseReqDetailsViewModel> models)
        {

            var result = new SaveUpdateValueViewModel();
            //foreach (var model in models)
            //{
            //    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpUpdateSalesInvoiceForApproval {userId}, {model.purchaseReqId},{model.purchaseReqDetailsId},{model.reqQty},{model.Total}").AsNoTracking().FirstOrDefaultAsync();
            //}
            foreach (var purReqDetailsViewModel in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseReqDetailsForApproval {userId},{purReqDetailsViewModel.purchaseReqDetailsId},{purReqDetailsViewModel.purchaseReqId},{purReqDetailsViewModel.productReqDetailsId},{purReqDetailsViewModel.productId},{purReqDetailsViewModel.productWiseSpecificationId},{purReqDetailsViewModel.reqQty},{purReqDetailsViewModel.price},{purReqDetailsViewModel.isActive},{purReqDetailsViewModel.prodSpecification},{purReqDetailsViewModel.purchaseOrderDetailId},{purReqDetailsViewModel.receivedQty},{purReqDetailsViewModel.currentStockQty},{purReqDetailsViewModel.vatAmount}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseReqMasterListForApproval(string userId, int purchaseReqId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqMasterApprovalJson {userId}, {purchaseReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseReqDetailsByIdForApproval(int purchaseReqId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqDetailsForApprovalJSON {purchaseReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseReqMasterListByStatus(string userId, int status)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceMasterListByStatusJson {userId}, {status}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        #endregion


        #region purchase requisition report--------
        public async Task<JsonViewModel> GetPurchaseRequisitionGridReport(int? purchaseReqId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqGridReportInfo {purchaseReqId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #endregion



        #region  Quotation Collection

        public async Task<bool> DeleteQuotationCollectionById(string id, int quotationCollectionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteQuotationCollection {id}, {quotationCollectionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetQuotationCollectionById(int? userId, int? quotationCollectionId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetQuotationCollectionJSON {userId},{quotationCollectionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveQuotationCollection(string id, QuotationCollectionViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetQuotationCollection {id}, {model.quotationCollectionMasterId}, {model.quotationCollectionMasterDate},{model.PurRequisitionFinalizeDetailId},{model.status},{model.remarks},{model.quotationTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Quotation Collection Details


        public async Task<bool> DeleteQuotationCollDetailsById(string id, int purQuoCollDetailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteQuotationCollDetailsById {id}, {purQuoCollDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetQuotationCollDetailsById(int? purReqDetailsId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseReqDetailsJSON {purReqDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetQuotationCollDetailsByMasterId(int? masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetQuotationCollDetailsByMasterId {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveQuotationCollDetails(string id, List<QuotationCollectionDetailsViewModel> dataList, int quotationCollectionId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (QuotationCollectionDetailsViewModel model in dataList)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetQuotationCollDetails {id},{model.quotationCollectionDetailId},{quotationCollectionId},{model.supplierId},{model.qty},{model.rate},{model.isActive},{model.deferredRate},{model.manufactureOrigin},{model.PurRequisitionFinalizeDetailId},{model.productWiseSpecificationId},{model.VatAmount},{model.VatPercent},{model.TotalRate},{model.BudgetCreateId},{model.Discount}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseFinalReqDetailByMasterIdForPdfReport(int? finalRequisitionId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetFinalizeRequisitionDetailByMasterIdForPdfReport {finalRequisitionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        #endregion

        #region requisition Approval Matrix

        public async Task<int> SavePurchaseApprovalMatrix(string empid, List<PurchaseApprovalMatrixViewModel> leaveApprovalMatrixViewModels, int? employeeId, int? deptId, int? productTypeId)
        {
            try
            {

                await _context.saveUpdateViewModels.FromSql($"HrmSpDeletePurchaseApprovalMatrix {empid},{employeeId},{deptId},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();

                var result = new SaveUpdateValueViewModel();
                foreach (PurchaseApprovalMatrixViewModel model in leaveApprovalMatrixViewModels)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetPurchaseApprovalMatrix {empid},{model.approvalMatrixId},{employeeId},{model.approverId},{model.isFinalApproval},{model.seqNo},{model.isActive},{deptId},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
                }

                if (result.isSuccess > 0)
                {
                    var setDefaultApproverMatrix = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetDefaultApproverForApprovalType {empid},{result.isSuccess},{productTypeId}, {"PurchaseReqApproverHO"}").AsNoTracking().FirstOrDefaultAsync();
                }

                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetPurchaseApprovalMatrix(int? id, int? empId, int? productTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetPurchaseApprovalMatrixByemployeeIdJson {id},{empId},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeletePurchaseApprovalMatrixByTypeId(string id, int? employeeId, int? productTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeletePurchaseApprovalMatrix {id},{employeeId},{0},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion requisition Approval Matrix
    }
}
