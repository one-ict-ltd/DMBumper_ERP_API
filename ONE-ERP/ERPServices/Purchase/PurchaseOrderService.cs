using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Purchase.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ERPDbContext _context;
        public PurchaseOrderService(ERPDbContext context)
        {
            _context = context;
        }

        #region Prodct Order Master

        public async Task<bool> DeletePurchaseOrderById(string id, int purOrderId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseOrder {id}, {purOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseOrderById(int? purOrderId, int? purchaseTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderInfoJSON {purOrderId},{purchaseTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseOrderDataById(int? purOrderId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderDetailsDataJSON {purOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        public async Task<JsonViewModel> GetPurchaseOrderBypurchaseOrderId(int? purOrderId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderDetailsInUpdate {purOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SavePurchaseOrder(string id, PurchaseOrderViewModel purOrderViewModel)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseOrder {id}, {purOrderViewModel.purchaseOrderId}, {((purOrderViewModel.purchaseReqId == 0) ? null : purOrderViewModel.purchaseReqId)},{purOrderViewModel.purchaseOrderDate},{((purOrderViewModel.fromWarehouseId == 0) ? null : purOrderViewModel.fromWarehouseId)},{((purOrderViewModel.toWarehouseId == 0) ? null : purOrderViewModel.toWarehouseId)},{purOrderViewModel.purchaseOrderFromId},{purOrderViewModel.approvalStatus},{purOrderViewModel.purpose},{purOrderViewModel.isUrgency},{purOrderViewModel.isActive},{purOrderViewModel.supplierId},{purOrderViewModel.lcNo},{purOrderViewModel.refNo},{purOrderViewModel.transactionTypeId},{purOrderViewModel.purchaseFromId},{purOrderViewModel.csMasterId},{purOrderViewModel.requisitionFinalizeMasterId}, {purOrderViewModel.grossAmount}, {purOrderViewModel.totalVat}, {purOrderViewModel.totalAit}, {purOrderViewModel.totalDiscount}, {purOrderViewModel.freightCharge}, {purOrderViewModel.netAmount}, {purOrderViewModel.purchaseOrderSignatoryId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        #endregion

        #region Prodct Order Details

        public async Task<bool> DeletePurchaseOrderDetailsById(string id, int purOrderDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseOrderDetails {id}, {purOrderDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseOrderDetailsById(int? purOrderDetailsId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderDetailsJSON {purOrderDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SavePurchaseOrderDetails(string id, List<PurchaseOrderDetailsViewModel> purPurchaseOrderDetailsViewModels, int purchaseOrderId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (PurchaseOrderDetailsViewModel model in purPurchaseOrderDetailsViewModels)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPurchaseOrderDetails {id},{model.purchaseOrderDetailsId},{purchaseOrderId},{model.purchaseReqDetailsId},{model.productId},{model.productWiseSpecificationId},{model.reqQty},{model.price},{model.requisitionFinalizeDetailId},{model.csDetailId},{model.vatPercent},{model.aitPercent},{model.discountPercent},{model.amount},{model.vatAmount},{model.BudgetCreateId},{model.prodSpecification}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        #endregion

        #region  Terms && Conditions 
        public async Task<int> SaveTermsAndConditions(string Id, List<TermsAndConditionsViewModel> termsandconditions, int supplierId, int productTypeId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (TermsAndConditionsViewModel model in termsandconditions)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetTermsAndConditions {Id},{model.termsAndCoditionsId},{model.supplierId},{model.termsAndConditions},{model.productTypeId}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetTermsAndConditionsById(int Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetTermsAndConditionsJson {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetTermsAndConditionsNoStuffById(int supplierId, int productTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetTermsAndConditionsNoStuffJson {supplierId},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetProductTypeWiseTermsAndConditions(int purchaseOrderId, int productTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetProductTypeWiseTermsAndConditions {purchaseOrderId},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<bool> DeleteTermsAndConditionsById(string id, int termsAndConditionsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteTermsAndConditions {id},{termsAndConditionsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Purchase Order Wise Terms & Conditions

        public async Task<int> SavePOWisetermsAndConditions(string id, List<POWiseTermsAndConditionsViewModel> poWiseTermsAndConditions, int purchaseOrderId)
        {
            await _context.saveUpdateViewModels.FromSql($"purSpDeletePOWiseTermsAndConditions {id},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (POWiseTermsAndConditionsViewModel item in poWiseTermsAndConditions)
            {
                try
                {
                    if (item.Active == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetPOWiseTermsAndConditions {id},{purchaseOrderId},{item.termsAndConditions},{item.supplierId}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetTermsAndConditionsInUpdate(int Id)
        {
            var result = await _context.jsonViewModels.FromSql($"purSpGetTermsAndConditionsInUpdateJson {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Reports
        public async Task<JsonViewModel> GetPurchaseOrderNumberByType(int? reportTypeId, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderNumberByTypeJSON {reportTypeId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> SpGetPartyBySbu(int? reportTypeId, int? sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPartyBySbuJSON {reportTypeId},{sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDateRangeWisePoEntryUser(DateTime? fromDate, DateTime? toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetDateRangeWisePoEntryUserJSON {fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseOrdersReportData(int? salesInvoiceId, int? partyId, DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersReportDataJSON {salesInvoiceId}, {partyId}, {fromDate}, {toDate}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel>  GetPurchaseOrdersReportData(int? reportTypeId, int? sbuId, int? partyId, int? userId, DateTime? fromDate, DateTime? toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersReportDataJSON {reportTypeId},{sbuId}, {partyId}, {userId}, {fromDate}, {toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPurchaseOrdersReport(int? salesInvoiceId, int? partyId, DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersReportDataJSON {salesInvoiceId}, {partyId}, {fromDate}, {toDate}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPurchaseOrdersReport(int? supplierId, int? productTypeId, int? productId, int? userId, DateTime? fromDate, DateTime? toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersReportDataJSON {supplierId},{productTypeId}, {productId}, {fromDate}, {toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPOSearchResult(string SearchingText, DateTime? FromDate, DateTime? ToDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderSearchResult {SearchingText}, {FromDate}, {ToDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region GRN

        public async Task<bool> DeleteGRNById(string id, int grnId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteGRN {id}, {grnId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetGRNById(int? userId, int? grnId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNInfoJSON {userId},{grnId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetGRNForReturnOrderById(int? userId, int? grnId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNForReturnOrderById {userId},{grnId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getGRNImportById(int? grnId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNImportInfoJSON {grnId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getGRNImportForReturnOrder(int? grnId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNImportForReturnOrder {grnId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetGRNDetailsById(int? grnId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrderDetailsInUpdate {grnId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> SaveGRNImport(string id, GRNImportViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNImport {id}, {model.ImpgrnMasterId},{model.grnDate},{model.ImpPreLCInfoMasterId},{model.factoryReceivedDate},{model.RMRNo},{model.MRRNo},{model.TruckNo},{model.DriverName},{model.CFAgentName},{model.mobileNo},{model.rejectedGRN}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<int> SaveGRN(string id, GRNViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRN {id}, {model.grnMasterId},{model.grnDate},{model.purchaseOrderId},{model.inhouseChallanNo},{model.factoryReceiveSINo},{model.supplierChallanNo},{model.supplierChallanDate},{model.factoryReceivedDate},{model.rejectedGRN}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<bool> DeleteGRNDetailsById(string id, int grnDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeletePurchaseOrderDetails {id}, {grnDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveGRNImportDetails(string id, List<GRNImportDetailsViewModel> detailsViewModel, int grnId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (GRNImportDetailsViewModel model in detailsViewModel)
            {
                try
                {
                    if (model.isSelect == true && model.actualRcvQty != null && model.actualRcvQty > 0)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNImportDetails {id},{model.grnDetailsId},{grnId},{model.PurImpPreLCInfoDetailId},{model.receivedQty},{model.price},{model.totalAmount},{model.vatPercent},{model.vatAmount},{model.actualAmount},{model.toUOMId},{model.actualRcvQty},{model.mfgDate},{model.expiryDate},{model.noOfBag},{model.batchNo},{model.manufactureOrigin},{model.QtyWithPackSize},{model.PrevQcReferenceNo}").AsNoTracking().FirstOrDefaultAsync();
                    }

                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
            return result.isSuccess;
        }

        public async Task<int> SaveGRNDetails(string id, List<GRNDetailsViewModel> detailsViewModel, int grnId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (GRNDetailsViewModel model in detailsViewModel)
            {
                try
                {
                    if (model.isSelect == true && model.actualRcvQty != null && model.actualRcvQty > 0)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetGRNDetails {id},{model.grnDetailsId},{grnId},{model.purchaseOrderDetailsId},{model.receivedQty},{model.price},{model.totalAmount},{model.vatPercent},{model.vatAmount},{model.actualAmount},{model.toUOMId},{model.actualRcvQty},{model.mfgDate},{model.expiryDate},{model.noOfBag},{model.batchNo},{model.manufactureOrigin},{model.QtyWithPackSize},{model.PrevQcReferenceNo}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> getGRNsupplierChallanNo(int? poId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetALLGRNsupplierChallanNo {poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPurchaseOrdersForGRN(int? poId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersForGRN {poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetPurchaseOrdersForGRNN(int? poId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersForGRNN {poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel>GetPurchaseOrdersForRejectedGRN(int? poId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetPurchaseOrdersForRejectedGRN {poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> getLcNo()
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetAllLcNo").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getLcNoForRejectedQty()
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetAllLcNoForRejectedQty").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPODetailsByIdForGRN(int? poId, int? grnMasterid)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNDetailsForPdfReport {grnMasterid},{poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetGRNImportDetails(int? lcId, int? grnMasterid)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetGRNImportDetails {grnMasterid},{lcId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getPODetailsByLcInfo(int? ImpPreLCInfoMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetGRNDetailsByLcInfo {ImpPreLCInfoMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPODetailsByIdForGRNForReport(int? poId, int? grnMasterid)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetPODetailsByIdForGRN  {grnMasterid},{poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetRejectedGRN(int? purchaseOrderId)
        {
            try {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetRejectedGRNbyPurchaseOrderId  {purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRejectedImportGRN(int? ImpPreLCInfoMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetRejectedImportGRNbyId  {ImpPreLCInfoMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Bill

        public async Task<bool> DeleteBillById(string id, int billId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteBill {id}, {billId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBillById(int? userId, int? billId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBillInfoJSON {userId},{billId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetBillByIdForPdfReport(int? billId)
        {
            var result = await _context.jsonViewModels.FromSql($"getSpBillDateforPdfReport {billId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetBillDetailsById(int? billId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBillDetailsById {billId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveBill(string id, BillViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetBill {id}, {model.billMasterId},{model.billDate},{model.partyId},{model.omrRmrNo},{model.omrRmrDate},{model.supplierBillNo},{model.supplierBillDate},{model.supplierChallanNo},{model.supplierChallanDate},{model.particular},{model.remarks},{model.grandTotal},{model.discountPercent},{model.discountAmount},{model.truckFair},{model.transportBill},{model.tdsPercent},{model.tdsAmount},{model.netAmount},{model.advancePaidAmount},{model.billStatus},{model.creditPeriod},{model.maturityDate}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> SaveBillDetails(string id, List<BillDetailsViewModel> detailsViewModel, int billId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (BillDetailsViewModel model in detailsViewModel)
            {
                try
                {
                    if (model.isSelect == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetBillDetails {id},{model.billDetailId},{billId},{model.grnDetailId},{model.receivedQty},{model.rate},{model.totalAmount},{model.vatPercent},{model.vatAmount},{model.actualAmount}").AsNoTracking().FirstOrDefaultAsync();
                    }

                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> getSupplierWiseProductsForBill(int? supplierId, int? billMasterid, int? poId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetSupplierWiseProductsForBill {billMasterid},{supplierId},{poId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getBillPayableJV(string userId,int? billMasterId, int? partyId, decimal paymentAmount, decimal vatPaymentAmount, decimal vdsPaymentAmount, decimal tdsPaymentAmount, decimal netPaymentAmount)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBillPayableJV {userId},{billMasterId},{partyId},{paymentAmount},{vatPaymentAmount},{vdsPaymentAmount},{tdsPaymentAmount},{netPaymentAmount}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getAllPOForBill(string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"EXEC [dbo].[PurSpGetAllProductsForBill] {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getSupplierWiseProductsForBillForPdfReport(int? supplierId, int? billMasterid)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetSupplierWiseProductsForBillforPdfReport {billMasterid},{supplierId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion


        #region Bill Payment
        public async Task<JsonViewModel> GetBillInfoForPayment(int? userId, int? billId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBillInfoForPaymentJSON {userId},{billId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteBillPaymentById(string id, int paymentMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteBillPayment {id}, {paymentMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBillPaymentById(int? userId, int? voucherMasterId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBillPaymentInfoJSON {userId},{voucherMasterId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetBillPayableVoucherById(int? userId, int? voucherMasterId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBillPayableVoucherInfoJSON {userId},{voucherMasterId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveBillPayableVoucherPosting(string id, BillPayableViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetBillPayableVoucher {id}, {model.billMasterId},{model.voucherDate},{model.billNo},3,{model.voucherRemarks},{model.paymentAmount},{model.vatPaymentAmount},{model.vdsPaymentAmount},{model.tdsPaymentAmount},{model.vdsPercent},{model.tdsPercent},{model.advancePaidAmount},{model.discountAmount}").AsNoTracking().FirstOrDefaultAsync();
                int voucherMasterId = result.isSuccess;
                foreach (VoucherDetailViewNModel voucherDetailViewModel in model.lstdetailmodel)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVoucherDetails {id},{0},{voucherMasterId},{voucherDetailViewModel.ledgerId},{voucherDetailViewModel.partyId},{voucherDetailViewModel.amount},{voucherDetailViewModel.transactionModeId},{voucherDetailViewModel.isPrinAcc},{voucherDetailViewModel.isActive},{""},{voucherDetailViewModel.partyName},{voucherDetailViewModel.remarksDetail}").AsNoTracking().FirstOrDefaultAsync();

                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<JsonViewModel> getSupplierInfoForBillPayment(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetSupplierInfoForBillPaymentJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveBillPayment(string id, BillPaymentsViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetBillPaymentVoucher {id},{model.voucherDate},{model.billNo},2,{model.voucherRemarks},{model.partyId},{model.accountId},{model.paymentAmount}").AsNoTracking().FirstOrDefaultAsync();
                int voucherMasterId = result.isSuccess;
                foreach (BillPaymentsDetailsViewModel detailViewModel in model.lstDetailsViewModel.Where(x => x.isSelect == true))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetPurPaymentMaster {id},{detailViewModel.partyId},{detailViewModel.billMasterId},{model.voucherDate},{voucherMasterId},{detailViewModel.paymentAmount}").AsNoTracking().FirstOrDefaultAsync();

                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<JsonViewModel> GetSupplierWiseBillsForPayment(int? userId,int? supplierId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetSupplierWiseBillsForPayment {userId},{supplierId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        #endregion

        #region Budget Create
        public async Task<JsonViewModel> GetBudgetCategoryList()
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetBudgetCategoryListJSON").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveBudgetCreate(string id, List<PurBudgetCreateViewModel> PurBudgetCreateViewModel)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (PurBudgetCreateViewModel model in PurBudgetCreateViewModel)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetBudgetCreate {id},{model.BudgetAmount},{model.BudgetCategoryId},{model.BudgetYear},{model.BudgetCreateId}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetBudgetCreateList(int? BudgetCreateId)
        {

            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetBudgetCreateList {BudgetCreateId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        #endregion

        #region Create Purchase Auto Voucher       

        #region Cash
        public async Task<int> CreateAutoJournalForPurchase(string id, PurchaseOrderViewModel model,int purchaseOrderId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreatePurchaseJournal {id},{model.purchaseOrderDate},{model.supplierId},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<int> CreateAutoJournalForPurchaseDirect(string id, PurchaseViewModel model, int purchaseOrderId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreatePurchaseJournal {id},{model.purchaseOrderDate},{model.supplierId},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        #endregion

        #region Credit
        public async Task<int> CreateAutoJournalForPurchaseOnCredit(string id, PurchaseOrderViewModel model, int purchaseOrderId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreatePurchaseJournalOnCredit {id},{model.purchaseOrderDate},{model.supplierId},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<int> CreateAutoJournalForPurchaseDirectOnCredit(string id, PurchaseViewModel model, int purchaseOrderId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreatePurchaseJournalOnCredit {id},{model.purchaseOrderDate},{model.supplierId},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        #endregion

        #region Advance
        public async Task<int> CreateAutoJournalForPurchaseOnAdvance(string id, PurchaseOrderViewModel model, int purchaseOrderId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreatePurchaseJournalOnAdvance {id},{model.purchaseOrderDate},{model.supplierId},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<int> CreateAutoJournalForPurchaseDirectOnAdvance(string id, PurchaseViewModel model, int purchaseOrderId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreatePurchaseJournalOnAdvance {id},{model.purchaseOrderDate},{model.supplierId},{purchaseOrderId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        #endregion


        #endregion
    }
}
