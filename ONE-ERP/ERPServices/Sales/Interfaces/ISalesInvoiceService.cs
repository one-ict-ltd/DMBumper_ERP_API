using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesInvoiceService
    {
        #region SalesInvoiceService Master

        Task<JsonViewModel> GetSalesOrderMasterApprovedList(string userId, int masterId, string territoryCode);
        Task<JsonViewModel> GetSalesOrderDetailsByIdForApproval(int salesInvoiceId);
        Task<string> ValidateCurrentStockForOrder(int? userId, int orderId, int? storeId, int? productWiseSpecificationId, decimal? invoiceQty);
        Task<string> ValidateCustomerDuesStatusForOrder(int? userId, int orderId);
        Task<int> GenerateSalesInvoiceBySalesOrder(int? userId, GenerateInvoiceViewModel models);
        Task<int> GenerateSalesInvoiceBySalesOrder_v2(int? userId, GenerateInvoiceViewModel models);
        Task<int> SaveSalesInvoice(string id, SalesInvoiceViewModel model);
        Task<int> SaveMoneyReceiptNote(string id, MoneyReceiptNoteViewModel model);
        Task<JsonViewModel> ValidateMoneyReceiptNoteTrxnNo(int? userId, string trxnNo);
        Task<int> SaveMoneyReceipt(string id, MoneyReceiptViewModel model);
        Task<int> DeleteMoneyReceiptDetails(int masterId);
        Task<int> SaveMoneyReceiptDetails(string id, List<MoneyReceiptDetailsViewModel> detailsModel, int masterId);
        Task<JsonViewModel> GetSalesInvoiceById(int? salesInvoiceId, int? userId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetInvoiceGDNConfirmation(int? salesInvoiceId, int? userId, DateTime? fDate, DateTime? tDate, int gdnType);
        Task<string> GetValidateProductStockForInvoice(string userId, int? storeId, int? productWiseSpecificationId, string batchNo, decimal? invoiceQty, int? partyId, DateTime? salesInvoiceDate, bool? hasNationalBonus);
        Task<int> SaveInvoiceGDNConfirmation(string id, string salesInvoiceIds, int gdnType);
        Task<int> SaveGDNConfirmationLogs(string id, string salesInvoiceIds);
        Task<JsonViewModel> GetSalesInvoiceForPosById(int? salesInvoiceId);
        Task<JsonViewModel> GetMaxSalesInvoiceNumber(int userId, DateTime datetime);
        Task<JsonViewModel> GetCurrentStock(int storeId, int productWiseSpecificationId, string batchNo);
        Task<JsonViewModel> SetCurrentStock(int storeId, string productCode, decimal ProposedStockQty, string batchNo);
        Task<JsonViewModel> GetProductBatch(int storeId, int productWiseSpecificationId);
        Task<JsonViewModel> GetItemWsieBonus(int? partyId, int? productWiseSpecificationId, DateTime? invoiceDate, decimal? invQty);
        Task<JsonViewModel> GetCollectionDiscountNotApplicableProductList(int? userId, int? partyId);
        Task<bool> DeleteSalesInvoiceById(string id, int salesInvoiceId);
        Task<bool> DeleteGDNById(string id, int salesInvoiceId);
        Task<bool> DeleteSalesPicking(int? userId, int pickingMasterId);
        Task<bool> DeleteDispatch(int? userId, int masterId);
        Task<JsonViewModel> GetProductSerialNoByProductSpec(int productWiseSpecificationId);
        Task<JsonViewModel> GetSalesInvoiceAmountById(int salesInvoiceId);
        Task<JsonViewModel> GetSalesInvoiceByPartyId(int partyId);
        Task<JsonViewModel> GetMoneyReceiptType(int? userId);
        Task<JsonViewModel> GetMaxMoneyReceiptNo(int? userId, DateTime? invoiceDate);
        Task<JsonViewModel> GetAllMoneyReceiptNote(int? userId, int? masterId, DateTime? tdate, DateTime? fdate);
        Task<JsonViewModel> GetAllMoneyReceipt(int? userId, int? masterId, DateTime? tdate, DateTime? fdate);
        Task<JsonViewModel> GetAllMoneyReceiptDetails(int? masterId);
        Task<JsonViewModel> GetAllPendingMoneyRecipts(int? userId, string territoryCode, string mioCode);
        Task<JsonViewModel> GetAllPendingMoneyReciptsNew();
        Task<JsonViewModel> GetAllPendingMoneyReciptsForBill(int? userId);
        Task<int> SaveSalesPicking(string id, int PickingMasterId, DateTime? pickingDate);
        Task<int> SaveSalesPickingSammary(string id, int PickingMasterId, int salesInvoiceId);
        Task<int> SaveSalesPickingDetails(string id, int PickingMasterId, int productWiseSpecificationId, decimal? invoiceQty);
        Task<JsonViewModel> GetSalesPickingMasterListJson(string userId, int pikingMasterId);
        Task<int> SaveSalesDispatch(string id, int dispatchMasterId, int? employeeId, DateTime? date);
        Task<JsonViewModel> GetSalesDispatchDetailsbyId(int distributionMasterId);
        Task<int> SaveSalesDispatchDetails(string id, int dispatchMasterId, int pickingMasterId, int? salesInvoiceId);
        Task<JsonViewModel> GetSalesInvoiceListfromDispatchJson(string userId, int dispatchMasterId);
        Task<JsonViewModel> GetSalesInvoiceListfromDispatchJson_v2(string userId, int dispatchMasterId, int? partyId, DateTime? collectionDate, string territoryCode, int? transactionTypeId, string mioCode);
        Task<JsonViewModel> GetSalesInvoiceListForBillCollection(string userId, int? collectionMasterId, int? partyId, DateTime? collectionDate);
        Task<JsonViewModel> GetSalSpGetAllPickingJson(int? userId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> SalSpGetAllPickingDetailsByMasterIdJson(string userId, int pikingMasterId);
        Task<JsonViewModel> GetSalesPickingSummaryByMasterIdJson(string userId, int pikingMasterId);
        Task<JsonViewModel> GetSalSpGetAllSalesDispatchJson(int employeeId, DateTime? fromdate, DateTime? toDate);
        Task<JsonViewModel> GetSalSpGetAllSalesDispatchByIdJson(string userId, int masterId);
        Task<JsonViewModel> GetAllPartysByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode);
        Task<JsonViewModel> GetAllActivePartysByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode);
        Task<JsonViewModel> GetAllActivePartysForChallanByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode);
        Task<JsonViewModel> GetAllActivePartysForBillByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode);
        Task<JsonViewModel> GetAllMIOByTerritory(int? userId, string territoryCode);
        Task<JsonViewModel> GetSalesInvoiceMasterListByStatusandTerritory(string userId, int status, string territoryCode);
        Task<JsonViewModel> GetAllDepot(int? userId);
        Task<JsonViewModel> GetSalesDashboardChartData(int? userId);
        Task<JsonViewModel> GetSalesDashboardDueChartData(int? userId);
        Task<JsonViewModel> GetSalesDashboardData(DateTime? fromDate, DateTime? toDate, string userId);
        Task<JsonViewModel> GetSalesDashboardDataDetails(DateTime? fromDate, DateTime? toDate, int userId, int type, int partyId);
        Task<JsonViewModel> GetSalesDashboardDataDetailsPartyWise(DateTime? fromDate, DateTime? toDate, int userId, int type);
        Task<JsonViewModel> GetTargetVsAchievementReport(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate);

        Task<JsonViewModel> GetNationalOutStandingReport(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, string reportFormat, int isJsonOutput,int isDuesAmtOnly, string invoiceNo, string mioCode);

        #endregion

        #region Tender Quotation
        Task<int> SaveTenderQuotation(string id, TenderQuotationViewModel model);
        Task<int> SaveTenderQuotationDetails(string id, List<TenderQuotationDetailsViewModel> models, int quotationMasterId);
        Task<JsonViewModel> GetTenderQuotationId(int? quotationMasterId, int? userId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetTenderQuotationDetailsById(int? quotationMasterId);
        Task<bool> DeleteTenderQuotationById(string id, int quotationMasterId);
        #endregion

        #region  Tender Quotation Approval
        Task<JsonViewModel> GetALLTenderQuotationApproval(int? id, int? isApproved);
        Task<int> SaveTenderQuotationApproval(int? userId, TenderQuotationApprovalViewModel model);
        #endregion Tender Quotation Approval

        #region Tender Challan
        Task<int> SaveTenderChallan(string id, TenderChallanViewModel model);
        Task<int> SaveTenderChallanDetails(string id, List<TenderChallanDetailsViewModel> models, int challanMasterId);
        Task<int> SaveTenderBill(string id, TenderBillViewModel model);
        Task<int> SaveTenderBillDetails(string id, List<TenderBillDetailsViewModel> models, int billMasterId);
        Task<int> SaveTenderFinalChallanDetails(string id, List<TenderFinalChallanDetailsViewModel> models, int challanMasterId);
        Task<JsonViewModel> GetTenderChallanById(int? challanMasterId, int? userId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetTenderBillById(int? billMasterId, int? userId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetQuotationForChallan(int? userId, int? partyId);
        Task<JsonViewModel> GetTenderQuotationDetailsForChallanById(int? quotationMasterId);
        Task<JsonViewModel> GetChallanForBill(int? userId, int? partyId);
        Task<JsonViewModel> GetChallanDetailsForBillById(int? challanMasterId);
        Task<JsonViewModel> GetTenderChallanDetailsForFinalChallanByQuotationMasterId(int? quotationMasterId);

        Task<JsonViewModel> GetTenderChallanWihoutQuotationById(int? challanMasterId, int? userId, DateTime? fDate, DateTime? tDate);
        #endregion

        #region SalesInvoiceService Details

        Task<int> SaveSalesInvoiceDetails(string id, List<SalesInvoiceDetailsViewModel> Model, int salesInvoiceId, int storeId, int companyId);
        Task<JsonViewModel> GetSalesInvoiceDetailsByMasterId(int? salesInvoiceId);
        //Task<JsonViewModel> GetAllPartysByTypeId(int? partyTypeId, int? sbuId);
        Task<JsonViewModel> GetPartyDetailsById(int? partyId);
        Task<JsonViewModel> GetProductSpecDetailsBySpecId(int? productSpecId);
        Task<bool> DeleteSalesInvoiceDetailsById(string id, int salesInvDetailsId);
        Task<bool> DeleteMoneyReceiptNoteById(int? id, int masterId);
        Task<bool> DeleteMoneyReceiptById(int? id, int masterId);
        Task<JsonViewModel> GetBarcodeDetails(string barcodeNo);
        Task<JsonViewModel> GetCustomerDuesStatus(int? userId, int partyId, string territoryCode);
        #endregion

        #region T&C

        Task<int> SaveSalesInvoiceTC(string id, List<SalesInvoiceTandCViewModel> Model, int salesInvoiceId);
        Task<JsonViewModel> GetSalesInvoiceTCByMasterId(int? salesInvoiceId);
        Task<bool> DeleteSalesInvoiceTCById(string id, int? salesInvoiceTCId, bool? isSelect);

        #endregion

        #region Reports
        Task<JsonViewModel> GetDateRangeWiseUserName(DateTime? fromDate, DateTime? toDate, int employeeId);
        Task<JsonViewModel> GetSalesInvoiceListByPartyId(int? partyId, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetAddressForReportFooter(int? companyId);
        Task<JsonViewModel> GetSalesInvoiceReportData(int? salesInvoiceId, int? partyId, DateTime? fromDate, DateTime? toDate, string userId);
        Task<JsonViewModel> GetSalesInvoiceReportDataById(int? salesInvoiceId);//, int? partyId, DateTime? fromDate, DateTime? toDate, string userId);
        Task<JsonViewModel> GetSalesReportByInvId(int? salesInvoiceId);
        Task<JsonViewModel> GetSalesInvoiceSearchResult(string SearchingText, DateTime? FromDate, DateTime? ToDate);

        Task<JsonViewModel> GetSaleRegisterReport(int? userId, string depoCode, string territoryCode, int? partyId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode);
        Task<JsonViewModel> GetSaleRegisterReportForBill(int? userId, int? partyId, DateTime? fDate, DateTime? tDate);

        Task<JsonViewModel> GetZoneRegionWiseSalesCollectionBalanceReport(int? userId, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime fDate, DateTime tDate, string type, string mioType);
        Task<JsonViewModel> GetMioProductSalesReport(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, int? partyId, int? productWiseSpecificationIdId);
        Task<JsonViewModel> GetZone(int? userId);
        Task<JsonViewModel> GetRegion(int? userId, string zoneCode);
        Task<JsonViewModel> GetTerritory(int? userId, string areaCode);
        Task<JsonViewModel> GetArea(int? userId, string regionCode);
        Task<JsonViewModel> GetAreaForNationalSalesReport(int? userId, string regionCode);
        Task<JsonViewModel> GetProductWiseNationalSalesReport(int? userId, DateTime? fDate, DateTime? tDate, string depoCode, string territoryCode, int? partyId);

        Task<JsonViewModel> GetNationalProductSalesReport(int? userId, DateTime? fDate, DateTime? tDate, string depoCode, string territoryCode, int? partyId);
        Task<JsonViewModel> GetWeeklyProductMonitorReport(int? userId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, string empCode);

        #endregion

        #region Approval

        Task<int> ApproveSalesInvoiceMaster(string userId, string approvalStatus, List<SalesInvoiceViewModel> models);
        Task<int> UpdateSalesInvoiceDetails(string userId, List<SalesInvoiceDetailsViewModel> models);
        Task<JsonViewModel> GetSalesInvoiceMasterListForApproval(string userId, int partyId);
        Task<JsonViewModel> GetSalesInvoiceDetailsByIdForApproval(int salesInvoiceId);
        Task<JsonViewModel> GetSalesInvoiceMasterListByStatus(string userId, int status, string territoryCode, int? transactionTypeId, string areaCode);

        #endregion

        #region Create Sales Auto Voucher  

        Task<int> CreateAutoJournalForSalesInvoice(string id, SalesInvoiceViewModel model);
        Task<int> CreateAutoJournalForSalesInvoiceOnCredit(string id, SalesInvoiceViewModel model);
        Task<int> CreateAutoJournalForSalesInvoiceOnAdvance(string id, SalesInvoiceViewModel model);

        #endregion

        #region For Android App

        Task<JsonViewModel> GetSalesOrderDtlByInvIdForApp(int? salesInvoiceId);
        Task<JsonViewModel> GetSalesOrderByChemist(int? chemistId, int? statusId, string fromDate, string toDate, int employeeId);
        Task<bool> UpdateSalesOrderStatusForApp(string id, int salesInvoiceId, int statusId);
        Task<JsonViewModel> GetSalesOrderByAdminForApprove(int? employeeId, int? statusId, string fromDate, string toDate);

        Task<int> ApproveSalesOrderStatusByAdmin(string userId, List<SalesInvoiceApproveViewModel> models);

        #endregion

        #region Sales party
        Task<int> SaveParty(string userId, SalesInvPartyViewModel model);
        Task<JsonViewModel> GetDuplicatePartyInfo(string partyName, string mobileNo);
        #endregion

        #region miscellaneouAndDamageExpiry
        Task<JsonViewModel> GetMiscellaneousItemDepotListJson(string userId, int typeId);
        Task<JsonViewModel> GetMiscellaneousItemMarketListJson(string userId);
        Task<int> SaveDamageExpireProductsReturn(string id, int? damageExpireProductReturnMasterId, int? miscellaneousTypeId, DateTime? date, string MarketOrDepo);
        Task<int> SaveDamageExpireProductsReturnDetails(string id, int damageExpireProductReturnMasterId, int MiscellaneousItemDetailId,decimal qty,int productSpecificationId);
        Task<int> DestructionNoteApproval(int? userId, DestructionNoteApprovalViewModel model);
        Task<JsonViewModel> GetAllDamageExpireProductReturn(string MarketOrDepo, int? employeeId, int? isApproved);
        Task<JsonViewModel> GetAllDestructionNoteReceive(int? employeeId, int? masterId);
        Task<JsonViewModel> GetAllDamageExpireReturnByIdJson(string userId, int masterId,string MarketOrDepo);
        #endregion

        #region Sales Report Nationally
        Task<JsonViewModel> GetSalesReportNationally(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, int reportPeriod);
        Task<string> GetSalesReportNationallyExcelOnly(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, int reportPeriod, string reportTypeName, string zoneName, string regionName, string territoryName, string areaName, string productName);
        Task<string> GetRptAccountScheduleReportByAccountGroupIdsExcelOnly(int companyId, int sbuId, string accountGroupIds, DateTime? fromDate, DateTime? toDate, string reportType, int? natureId, int? isOb, string reportFormat);
        Task<JsonViewModel> GetNationalSalesPerformance(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId);

        Task<JsonViewModel> GetNationSalesClosingStatement(int? userId, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId);
        Task<JsonViewModel> GetNationSalesClosingStatementLM(int? userId, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId);
        Task<JsonViewModel> GetNationalStockByQtyReport(int? userId, DateTime? fDate, int? productWiseSpecificationId, string productTypeName);
        Task<JsonViewModel> GetProductWiseSpecificationIdByName(int? userId, string productCode);

        #endregion

        #region Report Name
        Task<JsonViewModel> GetReportsName(int reportMasterId);
        Task<JsonViewModel> GetReportDetails(int dmsReportMasterId);
        #endregion

        #region Cash in Hand Report
        Task<JsonViewModel> GetCashInHand(string DepotCode, int? userId, DateTime fDate);
        #endregion

        #region Check Depot and Territory code
        Task<JsonViewModel> CheckDepotandTerritory(int? userId, string DepotCode, string territoryCode, string productCode);
        #endregion
        //Task<bool> SetPromoRequisitionUpload(int? userId, PromoRequisitionProductUploadViewModel program);
        //Task<bool> SetPromoRequisitionUploadDetails(int? userId, string DepotCode, string territoryCode, string productCode, decimal? quantity, string UploadId);


        #region Sales Order for Mobile App

        Task<JsonViewModel> GetSalesOrderById(int? salesOrderId, int? userId, DateTime? fDate, DateTime? tDate, int? approvalStatus, int? partyId);
        Task<bool> DeleteSalesOrderById(string id, int salesOrderId);
        Task<bool> DeleteSalesOrderDetailsByOrderDetailsId(int? id, int salesOrderDetailsId);
        Task<int> SaveSalesOrder(string id, SalesOrderViewModel model);
        Task<int> SaveSalesOrderDetails(string id, List<SalesOrderDetailsViewModel> Model, int salesOrderId, int storeId, int companyId);
        Task<int> SaveSalesOrderTC(string id, List<SalesOrderTandCViewModel> Model, int salesOrderId);
        Task<string> GetValidateProductAvailableStockForOrder(string userId, int? storeId, int? productWiseSpecificationId, string batchNo, decimal? invoiceQty, int? partyId, DateTime? salesOrderDate, bool? hasNationalBonus);
        Task<JsonViewModel> GetAvailableStockForOrder(int? userId, int storeId, int productWiseSpecificationId);
        #endregion

        #region Territory Sales Transfer
        Task<JsonViewModel> GetTerritoryForTerritoryTransfer(int TerritoryID , int employeeId);
        Task<bool> TransferTerritoryData(int? userId, string fromTerritoryCode,string toTerritoryCode);
        #endregion

        Task<int> UpdateFrizzProductStatus(string id, IEnumerable<FrizzProductViewModel> models);
        Task<bool> SetAppVersion(string id, int appVersion, int newVersion);
        Task<JsonViewModel> GetAppVersion(int? userId);

        #region SalesOrder Details
        Task<JsonViewModel> GetSalesOrderDetailsByMasterId(int? salesOrderId);
        #endregion
    }
}
