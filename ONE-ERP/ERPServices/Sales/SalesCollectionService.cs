using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesCollectionService : ISalesCollectionService
    {
        private readonly ERPDbContext _context;
        public SalesCollectionService(ERPDbContext context)
        {
            _context = context;
        }

        #region Sales Collection Master

        public async Task<bool> DeleteSalesCollectionById(string id, int collectionMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteCollectionInvoice {id}, {collectionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> DeleteSalesCollectionByMasterId(int? userId, int collectionMasterId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteCollectionByMasterId {userId}, {collectionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesCollectionById(int? salesCollectionId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionJSON {salesCollectionId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesCollectionById_v2(int? salesCollectionId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionJSON_v2 {salesCollectionId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesCollectionByIdJson(int? salesCollectionId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionByIdJSON {salesCollectionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetSalesCollectionByIdJson_v2(int? salesCollectionId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionByIdJSON_v2 {salesCollectionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCollectionAmountWiseCommissionPercent(int? userId, DateTime? collectionDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionAmountWiseCommissionPercent {userId}, {collectionDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxSalesCollectionNumber(DateTime datetime, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesCollectionNumberJson {datetime}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        // Tender/bill: territory/depot-free collection number (COL<yyMM>-<seq>)
        public async Task<JsonViewModel> GetMaxSalesCollectionNumberForBill(DateTime datetime, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesCollectionNumberForBillJson {datetime}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveSalesCollection(string id, SalesCollectionViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollection  {id}, {model.collectionMasterId}, {model.collectionNumber}, {model.partyId}, {model.salesInvoiceId}, {model.collectionDate}, {model.collectionAmount}, {model.remarks}, {model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveSalesCollectionDispatch(string id, SalesCollectionFromDispatchViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollection  {id}, {model.distributionMasterId}, {model.number}, {null}, {null}, {model.startDate}, {model.lstMasterViewModel.Where(x => x.isSelect == true).Sum(x => x.dueAmount)}, {"Collection from Dispatch"}, {true},{model.paymentModeId},{model.chequeNo},{model.chequeDate}").AsNoTracking().FirstOrDefaultAsync();

            for (int i = 0; i < model.lstMasterViewModel.Count(); i++)
            {
                //var x = $"SalSpSetSalesCollectionDetails {id},{model.lstMasterViewModel[i].collectionDetailId},{result.isSuccess},{2},{model.lstMasterViewModel[i].collectionAmount},{null},{null},{null},{true},{model.lstMasterViewModel[i].salesInvoiceId}";
                if (model.lstMasterViewModel[i].isSelect == true)
                {
                    await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollectionDetails {id},{model.lstMasterViewModel[i].collectionDetailId},{result.isSuccess},{2},{model.lstMasterViewModel[i].dueAmount},{""},{""},{""},{true},{model.lstMasterViewModel[i].salesInvoiceId},{model.lstMasterViewModel[i].bonusDiscount}").AsNoTracking().FirstOrDefaultAsync();

                    await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesCollectionVoucher {id},{model.lstMasterViewModel[i].dueAmount},{model.startDate},{result.isSuccess},{null},{model.lstMasterViewModel[i].salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            return result.isSuccess;
        }

        public async Task<int> SaveSalesCollection_v2(string id, SalesCollectionFromDispatchViewModel_v2 model)
        {
            try
            {

                // var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollection_v2  {id}, {model.collectionMasterId}, {model.collectionNumber}, {model.partyId}, {null}, {model.collectionDate}, {model.lstDetailsViewModel.Where(x => x.isSelect == true).Sum(x => (x.collectionAmount == null ? 0 : x.collectionAmount) + (x.bonusDiscount == null ? 0 : x.bonusDiscount) + (x.incentiveAmount == null ? 0 : x.incentiveAmount))}, {(string.IsNullOrEmpty(model.remarks) ? "" : model.remarks)}, {true},{model.paymentModeId},{model.chequeNo},{model.chequeDate}, {model.bankName},{model.branchName},{model.moneyReceiptNo}").AsNoTracking().FirstOrDefaultAsync();

                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollection_v2  {id}, {model.collectionMasterId}, {model.collectionNumber}, {model.partyId}, {null}, {model.collectionDate}, {model.lstDetailsViewModel.Where(x => x.isSelect == true).Sum(x => (x.collectionAmount == null ? 0 : x.collectionAmount) + (x.bonusDiscount == null ? 0 : x.bonusDiscount) + (x.incentiveAmount == null ? 0 : x.incentiveAmount) + (x.vatAdjustment == null ? 0 : x.vatAdjustment))}, {(string.IsNullOrEmpty(model.remarks) ? "" : model.remarks)}, {true},{model.paymentModeId},{model.chequeNo},{model.chequeDate}, {model.bankName},{model.branchName},{model.moneyReceiptNo},{model.moneyReceiptId}").AsNoTracking().FirstOrDefaultAsync();


                for (int i = 0; i < model.lstDetailsViewModel.Count(); i++)
                {
                    //var x = $"SalSpSetSalesCollectionDetails {id},{model.lstMasterViewModel[i].collectionDetailId},{result.isSuccess},{2},{model.lstMasterViewModel[i].collectionAmount},{null},{null},{null},{true},{model.lstMasterViewModel[i].salesInvoiceId}";

                    if (model.lstDetailsViewModel[i].isSelect == true)
                    {
                        await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollectionDetails_v2 {id},{model.lstDetailsViewModel[i].collectionDetailId},{result.isSuccess},{2},{model.lstDetailsViewModel[i].collectionAmount},{""},{""},{""},{true},{model.lstDetailsViewModel[i].salesInvoiceId},{model.lstDetailsViewModel[i].bonusDiscount},{model.lstDetailsViewModel[i].incentiveAmount},{model.lstDetailsViewModel[i].vatAdjustment},{model.lstDetailsViewModel[i].percentValue},{model.lstDetailsViewModel[i].productDiscountPercent}").AsNoTracking().FirstOrDefaultAsync();

                        await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesCollectionVoucher {id},{model.lstDetailsViewModel[i].collectionAmount},{model.collectionDate},{result.isSuccess},{null},{model.lstDetailsViewModel[i].salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return 0;
                //throw;
            }
        }

        // Tender/bill save: identical to SaveSalesCollection_v2 but master uses the
        // territory/depot-free numbering SP (SalSpSetSalesCollection_v2ForBill).
        public async Task<int> SaveSalesCollection_v2ForBill(string id, SalesCollectionFromDispatchViewModel_v2 model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollection_v2ForBill  {id}, {model.collectionMasterId}, {model.collectionNumber}, {model.partyId}, {null}, {model.collectionDate}, {model.lstDetailsViewModel.Where(x => x.isSelect == true).Sum(x => (x.collectionAmount == null ? 0 : x.collectionAmount) + (x.bonusDiscount == null ? 0 : x.bonusDiscount) + (x.incentiveAmount == null ? 0 : x.incentiveAmount) + (x.vatAdjustment == null ? 0 : x.vatAdjustment))}, {(string.IsNullOrEmpty(model.remarks) ? "" : model.remarks)}, {true},{model.paymentModeId},{model.chequeNo},{model.chequeDate}, {model.bankName},{model.branchName},{model.moneyReceiptNo},{model.moneyReceiptId}").AsNoTracking().FirstOrDefaultAsync();

                for (int i = 0; i < model.lstDetailsViewModel.Count(); i++)
                {
                    if (model.lstDetailsViewModel[i].isSelect == true)
                    {
                        await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollectionDetails_v2 {id},{model.lstDetailsViewModel[i].collectionDetailId},{result.isSuccess},{2},{model.lstDetailsViewModel[i].collectionAmount},{""},{""},{""},{true},{model.lstDetailsViewModel[i].salesInvoiceId},{model.lstDetailsViewModel[i].bonusDiscount},{model.lstDetailsViewModel[i].incentiveAmount},{model.lstDetailsViewModel[i].vatAdjustment},{model.lstDetailsViewModel[i].percentValue},{model.lstDetailsViewModel[i].productDiscountPercent}").AsNoTracking().FirstOrDefaultAsync();

                        await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesCollectionVoucher {id},{model.lstDetailsViewModel[i].collectionAmount},{model.collectionDate},{result.isSuccess},{null},{model.lstDetailsViewModel[i].salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return 0;
                //throw;
            }
        }

        public async Task<JsonViewModel> GetDepotWiseOfficerSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDepotWiseOfficerSalesCollectionBalanceJSON {userId},{depotCode},{territoryCode},{fDate},{tDate},{mioCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTerritoryWiseOfficerSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetTerritoryWiseOfficerSalesCollectionBalanceJSON {userId},{depotCode},{territoryCode},{fDate},{tDate},{mioCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTerritoryOfficerWiseSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpGetTerritoryOfficerWiseSalesCollectionBalanceJSON {userId},{depotCode},{territoryCode},{fDate.Date},{tDate.Date},{mioCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                return new JsonViewModel
                {
                    data = "[]"
                };
            }

        }
        public async Task<JsonViewModel> GetDepotWiseSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string regionCode, string areaCode, string Type)
        {
            try
            {
                string sql = $"SalSpGetDepotWiseSalesCollectionBalanceJSON {userId},{depotCode},{territoryCode},{fDate},{tDate},{regionCode},{areaCode},{Type}";
                var result = await _context.jsonViewModels.FromSql($"SalSpGetDepotWiseSalesCollectionBalanceJSON {userId},{depotCode},{territoryCode},{fDate},{tDate},{regionCode},{areaCode},{Type}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<JsonViewModel> GetTerritoryOfficerWiseCustomerSalesCollectionBalance(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string mioCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetTerritoryWiseCustomerSalesCollectionBalanceJSON {userId},{depotCode},{territoryCode},{fDate},{tDate},{mioCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveTerritoryCollectionTarget(string id, TerritoryCollectionTargetMasterViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollectionTargetMaster  {id}, {model.terrColTargetMasterId}, {model.depotCode}, {model.startDate}, {model.endDate}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteTerritoryCollectionTargetById(string id, int collectionTargetMasterId)
        {
            var result = await _context.saveUpdateViewModels
                .FromSql($"SalSpDelAllTerColTarMasterById {id}, {collectionTargetMasterId}")
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveTerritoryCollectionTargetDetails(string id, List<TerritoryCollectionTargetDetailsViewModel> model, int collectionTargetMasterId)
        {
            int output = 0;
            await _context.saveUpdateValueViewModels
                    .FromSql($"SalSpDelAllTerColTarDetailByMasterId  {id}, {collectionTargetMasterId}")
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            foreach (var item in model)
            {
                var result = await _context.saveUpdateValueViewModels
                    .FromSql($"SalSpSetSalesCollectionTargetDetail  {id}, {collectionTargetMasterId}, {item.terrColTargetDetailId}, {item.territoryCode}, {item.targetAmount}")
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                output += result.isSuccess;
            }
            return output;
        }

        public async Task<JsonViewModel> GetTerritoryCollectionTargetByIdJson(int? collectionTargetMasterId)
        {
            var result = await _context.jsonViewModels
                .FromSql($"SalSpGetSalesCollectionTargetMaster {collectionTargetMasterId}")
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Sales Invoice Details
        public async Task<bool> DeleteSalesCollectionDetailsById(string id, int salesCollectionDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalCollectionDetails {id}, {salesCollectionDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesCollectionDetailsByMasterId(int? salesCollectionId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionDetailJSON {salesCollectionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveSalesCollectionDetails(string id, List<SalesCollectionDetailsViewModel> models, int collectionMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (SalesCollectionDetailsViewModel model in models)
            {
                try
                {
                    //if (model.collectionAmount > 0 && model.paymentModeId==1 && model.bankName != null || model.paymentModeId !=1 && model.trxNo != null)
                    if (model.collectionAmount > 0)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesCollectionDetails {id},{model.collectionDetailId},{collectionMasterId},{model.paymentModeId},{model.collectionAmount},{model.bankName},{model.chequeNo},{model.trxNo},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result.isSuccess;
        }
        //public async Task<JsonViewModel> GetAllTerritoryForDepot(int? userId, string depotCode)
        //{
        //    var result = await _context.jsonViewModels.FromSql($"SpGetAllTerritoryForDepot {userId},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
        //    return result;
        //}


        public async Task<JsonViewModel> GetAllTerritoryForDepot(int? userId, string depotCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetAllTerritoryForDepot {userId},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllTerritoryForDepot_v2(int? userId, string depotCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetAllTerritoryForDepot_v2 {userId},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPartybyTerritoryCode(string territoryCode, string depotCode)
        {
            
            var result = await _context.jsonViewModels.FromSql($"AccSpGetPartybyTerritoryCodeJson {territoryCode},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPartybyTerritoryCodeForCollection(string territoryCode, string depotCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetPartybyTerritoryCodeForCollectionJson {territoryCode},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPartybyTerritoryCodeForBillCollection(string territoryCode, string depotCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetPartyByTerritoryCodeForBillCollectionJson {territoryCode},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPartybyDepotCode(string depotCode)
        {

            var result = await _context.partyModel
                .FromSql($"EXEC AccSpGetPartybyDepotCodeJson {depotCode}")
                .AsNoTracking()
                .ToListAsync();

            var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

            return new JsonViewModel { data = jsonResult };
        }
        public async Task<JsonViewModel> GetMoneyReceiptNoStatus(string moneyReceiptNo)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetMoneyReceiptNoStatus {moneyReceiptNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        #endregion

        #region Report
        public async Task<JsonViewModel> GetCollectionListByPartyId(int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionListJSON {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetCustomerWiseSalesCollectionDues(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, bool? isDuesAmtOnly, string mioCode, DateTime? colFromDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCustomerWiseSalesCollectionDuesJSON {userId},{depotCode},{territoryCode},{partyId},{fDate},{tDate},{cUpToDate},{isOverDues},{isDuesAmtOnly},{mioCode},{colFromDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductWiseSalesDues(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, int? productWiseSpecificationId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductWiseSalesDuesJSON {userId},{depotCode},{territoryCode},{partyId},{fDate},{tDate},{cUpToDate},{isOverDues},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCustomerWiseCollection(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCustomerWiseCollectionJSON {userId},{depotCode},{territoryCode},{partyId},{fDate},{tDate},{cUpToDate},{isOverDues}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCustomerWiseCollectionSummary(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, string zoneCode, string regionCode, string areaCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCustomerWiseCollectionSummaryJSON {userId},{depotCode},{territoryCode},{partyId},{fDate},{tDate},{cUpToDate},{isOverDues},{zoneCode}, {regionCode}, {areaCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTerritoryOfficerWiseCollection(int? userId, string depotCode, DateTime fDate, DateTime tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetTerritoryOfficerWiseCollectionJSON {userId},{depotCode},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCustomerWiseSalesCollectionDuesSummary(int? userId, string depotCode, string territoryCode, int? partyId, DateTime fDate, DateTime tDate, DateTime? cUpToDate, bool? isOverDues, bool? isDuesAmtOnly, string mioCode, DateTime? colFromDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCustomerWiseSalesCollectionDuesSummaryJSON {userId},{depotCode},{territoryCode},{partyId},{fDate},{tDate},{cUpToDate},{isOverDues},{isDuesAmtOnly},{mioCode},{colFromDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDepotStockReportData(int? userId, string depotCode, int? productWiseSpecificationId, DateTime fDate, DateTime tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDepotStockReportJSON {userId},{fDate},{tDate},{depotCode},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCollectionAndSalesTargetVsAchievement(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionAndSalesTargetVsAchievementJSON {userId},{depotCode},{territoryCode},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetCollectionBonusReport(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"spGetCollectionBonusJSON {userId}, {depotCode}, {territoryCode}, {fDate}, {tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region bill collection report----------
        public async Task<JsonViewModel> GetRptBillCollection(int? collectionMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpRepGetBillCollectionByIdJSON {collectionMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRptBillCollectionRpt(DateTime? fromDate, DateTime? toDate, int? partyId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpRepGetBillCollectionReportJSON {fromDate}, {toDate},{partyId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Create Sales Collection Auto Receive Voucher      

        public async Task<int> CreateAutoJournalForSalesCollection(string id, SalesCollectionViewModel model, int collectionMasterId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesCollectionVoucher {id},{model.collectionAmount},{model.collectionDate},{collectionMasterId},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        #endregion
    }
}
