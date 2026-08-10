using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesCollectionService
    {
        #region SalesInvoiceService Master

        Task<bool> DeleteSalesCollectionById(string id, int collectionMasterId);
        Task<int> DeleteSalesCollectionByMasterId(int? userId, int collectionMasterId);
        Task<JsonViewModel> GetSalesCollectionById(int? salesCollectionId, int? userId);
        Task<JsonViewModel> GetSalesCollectionById_v2(int? salesCollectionId, int? userId, DateTime? fDate, DateTime? tDate);
        //Task<JsonViewModel> GetAllProductReqNumberBySbuId(int sbuId);
        Task<JsonViewModel> GetMaxSalesCollectionNumber(DateTime datetime, int?userId);
        Task<JsonViewModel> GetMaxSalesCollectionNumberForBill(DateTime datetime, int?userId);
        Task<int> SaveSalesCollection(string id, SalesCollectionViewModel model);

        Task<int> SaveSalesCollectionDispatch(string id, SalesCollectionFromDispatchViewModel model);
        Task<int> SaveSalesCollection_v2(string id, SalesCollectionFromDispatchViewModel_v2 model);
        Task<int> SaveSalesCollection_v2ForBill(string id, SalesCollectionFromDispatchViewModel_v2 model); 

        Task<int> SaveTerritoryCollectionTarget(string id, TerritoryCollectionTargetMasterViewModel model);
        Task<bool> DeleteTerritoryCollectionTargetById(string id, int collectionTargetMasterId);
        Task<int> SaveTerritoryCollectionTargetDetails(string id, List<TerritoryCollectionTargetDetailsViewModel> model, int collectionTargetMasterId);
        Task<JsonViewModel> GetTerritoryCollectionTargetByIdJson(int? collectionTargetMasterId);

        Task<JsonViewModel> GetSalesCollectionByIdJson(int? salesCollectionId);
        Task<JsonViewModel> GetSalesCollectionByIdJson_v2(int? salesCollectionId);
        Task<JsonViewModel> GetCollectionAmountWiseCommissionPercent(int? userId, DateTime? collectionDate);

        Task<JsonViewModel> GetDepotWiseOfficerSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode);
        Task<JsonViewModel> GetTerritoryWiseOfficerSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode);
        Task<JsonViewModel> GetTerritoryOfficerWiseSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode);
        Task<JsonViewModel> GetDepotWiseSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string regionCode, string areaCode, string Type);
        Task<JsonViewModel> GetTerritoryOfficerWiseCustomerSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode);

        Task<JsonViewModel> GetAllTerritoryForDepot(int? userId, string depotCode);
        Task<JsonViewModel> GetAllTerritoryForDepot_v2(int? userId, string depotCode);
        Task<JsonViewModel> GetPartybyTerritoryCode(string territoryCode, string depotCode);
        Task<JsonViewModel> GetPartybyTerritoryCodeForCollection(string territoryCode, string depotCode);
        Task<JsonViewModel> GetPartybyTerritoryCodeForBillCollection(string territoryCode, string depotCode);
        Task<JsonViewModel> GetPartybyDepotCode(string depotCode);
        Task<JsonViewModel> GetMoneyReceiptNoStatus(string moneyReceiptNo);

        #endregion

        #region SalesInvoiceService Details
        Task<bool> DeleteSalesCollectionDetailsById(string id, int salesCollectionDetailsId);
        Task<JsonViewModel> GetSalesCollectionDetailsByMasterId(int? salesCollectionId);

        Task<int> SaveSalesCollectionDetails(string id, List<SalesCollectionDetailsViewModel> models, int collectionMasterId);

        //<<<<<<< devShahid
        Task<JsonViewModel> GetRptBillCollection(int? collectionMasterId);
        Task<JsonViewModel> GetRptBillCollectionRpt(DateTime? fromDate, DateTime? toDate, int? partyId);
        //=======


        #endregion

        #region Report
        Task<JsonViewModel> GetCollectionListByPartyId(int? partyId);


        Task<JsonViewModel> GetCollectionBonusReport(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate);
        Task<JsonViewModel> GetCustomerWiseSalesCollectionDuesSummary(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, bool? isDuesAmtOnly, string mioCode, DateTime? colFromDate);
        Task<JsonViewModel> GetDepotStockReportData(int? userId, string depotCode, int? productWiseSpecificationId, DateTime fDate, DateTime tDate);
        Task<JsonViewModel> GetCustomerWiseSalesCollectionDues(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, bool? isDuesAmtOnly, string mioCode, DateTime? colFromDate);
        Task<JsonViewModel> GetCustomerWiseCollection(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues);
        Task<JsonViewModel> GetCustomerWiseCollectionSummary(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, string zoneCode, string regionCode, string areaCode);
        Task<JsonViewModel> GetTerritoryOfficerWiseCollection(int? userId, string depotCode,  DateTime fDate, DateTime tDate);
        Task<JsonViewModel> GetProductWiseSalesDues(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, int? productWiseSpecificationId);
        Task<JsonViewModel> GetCollectionAndSalesTargetVsAchievement(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate);
        #endregion

        #region Create Sales Collection Auto Receive Voucher  

        Task<int> CreateAutoJournalForSalesCollection(string id, SalesCollectionViewModel model,int collectionMasterId);

        #endregion
    }
}
