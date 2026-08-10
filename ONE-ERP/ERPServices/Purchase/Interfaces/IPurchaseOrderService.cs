using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IPurchaseOrderService
    {
        #region Purchase Order Master	
        Task<int> SavePurchaseOrder(string id, PurchaseOrderViewModel purOrderViewModel);
        Task<JsonViewModel> GetPurchaseOrderById(int? purOrderId, int? purchaseTypeId);
        Task<JsonViewModel> GetPurchaseOrderBypurchaseOrderId(int? purchaseOrderId);
        Task<bool> DeletePurchaseOrderById(string id, int purOrderId);
        #endregion
        #region Purchase Order Details	
        Task<int> SavePurchaseOrderDetails(string id, List<PurchaseOrderDetailsViewModel> purOrderDetailsViewModel, int purOrderId);
        Task<JsonViewModel> GetPurchaseOrderDetailsById(int? purOrderDetailsId);
        Task<bool> DeletePurchaseOrderDetailsById(string id, int purOrderDetailsId);
        #endregion
        #region Purchase Order Wise Terms & Conditions	
        Task<int> SavePOWisetermsAndConditions(string id, List<POWiseTermsAndConditionsViewModel> poWiseTermsAndConditions, int purOrderId);
        Task<JsonViewModel> GetTermsAndConditionsById(int Id);
        Task<JsonViewModel> GetTermsAndConditionsNoStuffById(int supplierId, int productTypeId);
        Task<JsonViewModel> GetProductTypeWiseTermsAndConditions(int purchaseOrderId, int productTypeId);
        #endregion
        #region Terms && Conditions	
        Task<int> SaveTermsAndConditions(string Id, List<TermsAndConditionsViewModel> termsandconditions, int supplierId, int productTypeId);
        Task<JsonViewModel> GetTermsAndConditionsInUpdate(int Id);

        Task<bool> DeleteTermsAndConditionsById(string id, int termsAndConditionsId);
        #endregion

        #region Report Data	
        Task<JsonViewModel> GetPurchaseOrderDataById(int? purOrderId);
        Task<JsonViewModel> GetPurchaseOrderNumberByType(int? reportTypeId, int? partyId);
        Task<JsonViewModel> SpGetPartyBySbu(int? reportTypeId, int? sbuId);
        Task<JsonViewModel> GetDateRangeWisePoEntryUser(DateTime? fromDate, DateTime? toDate);
        Task<JsonViewModel> GetPurchaseOrdersReportData(int? reportTypeId, int? sbuId, int? partyId, int? userId, DateTime? fromDate, DateTime? toDate);
        Task<JsonViewModel> GetPurchaseOrdersReport(int? supplierId, int? productTypeId, int? productId, int? userId, DateTime? fromDate, DateTime? toDate);
        Task<JsonViewModel> GetPOSearchResult(string SearchingText, DateTime? FromDate, DateTime? ToDate);

        #endregion

        #region Create Purchase Auto Voucher  

        #region Cash
        Task<int> CreateAutoJournalForPurchase(string id, PurchaseOrderViewModel model,int purchaseOrderId);
        Task<int> CreateAutoJournalForPurchaseDirect(string id, PurchaseViewModel model, int purchaseOrderId);
        #endregion

        #region Credit
        Task<int> CreateAutoJournalForPurchaseOnCredit(string id, PurchaseOrderViewModel model, int purchaseOrderId);
        Task<int> CreateAutoJournalForPurchaseDirectOnCredit(string id, PurchaseViewModel model, int purchaseOrderId);

        #endregion

        #region Advance
        Task<int> CreateAutoJournalForPurchaseOnAdvance(string id, PurchaseOrderViewModel model, int purchaseOrderId);
        Task<int> CreateAutoJournalForPurchaseDirectOnAdvance(string id, PurchaseViewModel model, int purchaseOrderId);

        #endregion

        #endregion

        #region GRN 
        Task<int> SaveGRN(string id, GRNViewModel model);
        Task<int> SaveGRNImport(string id, GRNImportViewModel model);
        Task<JsonViewModel> GetGRNById(int? userId,int? grnId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetGRNForReturnOrderById(int? userId, int? grnId);
        Task<JsonViewModel> getGRNImportById(int? grnId);
        Task<JsonViewModel> getGRNImportForReturnOrder(int? grnId);

        Task<bool> DeleteGRNById(string id, int grnId);

        Task<int> SaveGRNDetails(string id, List<GRNDetailsViewModel> detailsViewModel, int purOrderId);
        Task<int> SaveGRNImportDetails(string id, List<GRNImportDetailsViewModel> detailsViewModel, int grnId);
        Task<JsonViewModel> GetGRNDetailsById(int? grnId);
        Task<bool> DeleteGRNDetailsById(string id, int purOrderDetailsId);
        Task<JsonViewModel> GetPurchaseOrdersForRejectedGRN(int? poId);
        Task<JsonViewModel> getGRNsupplierChallanNo(int? poId);
        Task<JsonViewModel> GetPurchaseOrdersForGRN(int? poId);
        Task<JsonViewModel> GetPurchaseOrdersForGRNN(int? poId);
        Task<JsonViewModel> getLcNo();
        Task<JsonViewModel> getLcNoForRejectedQty();
        Task<JsonViewModel> GetPODetailsByIdForGRN(int? poId, int? grnMasterid);
        Task<JsonViewModel> getPODetailsByLcInfo(int? ImpPreLCInfoMasterId);
        Task<JsonViewModel> GetPODetailsByIdForGRNForReport(int? poId, int? grnMasterid);
        Task<JsonViewModel> GetGRNImportDetails(int? lcId, int? grnMasterid);
        Task<JsonViewModel> GetRejectedGRN(int? purchaseOrderId);
        Task<JsonViewModel> GetRejectedImportGRN(int? ImpPreLCInfoMasterId);
        #endregion

        #region Bill 
        Task<int> SaveBill(string id, BillViewModel model);
        Task<JsonViewModel> GetBillById(int? userId,int? billId);
        Task<JsonViewModel> GetBillByIdForPdfReport(int? billId);
        Task<bool> DeleteBillById(string id, int billId);
        Task<int> SaveBillDetails(string id, List<BillDetailsViewModel> detailsViewModel, int billId);
        Task<JsonViewModel> GetBillDetailsById(int? billId);
        Task<JsonViewModel> getSupplierWiseProductsForBill(int? supplierId, int? billMasterid, int? poId);
        Task<JsonViewModel> getBillPayableJV(string userId, int? billMasterId, int? partyId, decimal paymentAmount, decimal vatPaymentAmount, decimal vdsPaymentAmount, decimal tdsPaymentAmount, decimal netPaymentAmount);
        Task<JsonViewModel> getAllPOForBill(string userId);
        Task<JsonViewModel> getSupplierWiseProductsForBillForPdfReport(int? supplierId, int? billMasterid);
        #endregion

        #region Bill Payment
        Task<JsonViewModel> GetBillInfoForPayment(int? userId, int? billId);
        Task<int> SaveBillPayableVoucherPosting(string id, BillPayableViewModel model);
        Task<JsonViewModel> GetBillPayableVoucherById(int? userId, int? voucherMasterId, DateTime fromDate, DateTime toDate);
        Task<JsonViewModel> getSupplierInfoForBillPayment(int? userId);
        Task<int> SaveBillPayment(string id, BillPaymentsViewModel model);
        Task<JsonViewModel> GetBillPaymentById(int? userId, int? voucherMasterId, DateTime fromDate, DateTime toDate);
        Task<bool> DeleteBillPaymentById(string id, int billId);
        Task<JsonViewModel> GetSupplierWiseBillsForPayment(int? userId, int? supplierId);
        #endregion
        #region Budget Create
        Task<JsonViewModel> GetBudgetCategoryList();
        Task<int> SaveBudgetCreate(string id, List<PurBudgetCreateViewModel> PurBudgetCreateViewModel);
        Task<JsonViewModel> GetBudgetCreateList(int? BudgetCreateId);
        #endregion
    }
}