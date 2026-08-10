using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.ERPServices.Purchase;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class StockService : IStockService
    {
        JsonViewModel jv = new JsonViewModel();
        private readonly ERPDbContext _context;

        public StockService(ERPDbContext context)
        {
            _context = context;
        }

        #region stock in 

        public async Task<JsonViewModel> GetStockInById(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetStockInInfoJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetStockDetailsInById(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockDetailsInUpdateJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetStockDetailsWithOutPOInById(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockDetailsWithOutPOInUpdateJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetFactoryFGProductionDetailsByIn(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetFactoryFGProductionDetailsByInJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxMRNumber(DateTime MRDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxMRNumberJson {MRDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveStockIn(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockIn {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockNo},{stockInViewModel.stockDate},{1},{stockInViewModel.remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveStockInDetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            //await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockInDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetStockInDetails {id},{item.stockDetailsId},{stockMasterId},{item.poReceiveDetailsId},{item.productId},{item.productWiseSpecificationId},{item.poQty},{item.stockQty},{item.purchaseRate},{item.poReceiveId}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        public async Task<bool> DeleteStockInById(string id, int stockMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteStockIn {id}, {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteFactoryProductionStockIn(string id, int stockMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteFactoryProductionStockIn {id}, {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetPurchaseOrderReceiveDetailsById(int? poReceiveId)
        {
            var result = await _context.jsonViewModels.FromSql($"spGetPOReceiveDetailJson {poReceiveId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region STOCK IN WITH OUT PO
        public async Task<JsonViewModel> GetCurrentstockId(int? specificationId, int? storeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetCurrentStockJSON {specificationId}, {storeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetStockInWithOutPOById(int? userId, int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetStockInWithOutPOfoJSON {stockMasterId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetRmPmStockInWithOutPOById(int? userId, int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetRmPmStockInWithOutPOfoJSON {stockMasterId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetStockInWithProductionById(int? userId, int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetStockInWithProductionfoJSON {stockMasterId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetStockInWithProductionById_FromTransferNote(int? userId, int? stockMasterId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetStockInWithProductionfoJSON_FromTransferNote {stockMasterId}, {userId}, {fDate}, {tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetFactoryFGStockInJSON(int? userId, int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetFactoryFGStockInJSON {stockMasterId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetFactoryFGStockInJSONForStock(int? userId, int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetFactoryFGStockInJSONForStock {stockMasterId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveStockInWithOutPO(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockInWithOutPO {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockDate},{1},{stockInViewModel.remarks},{stockInViewModel.purchaseOrderNo},{stockInViewModel.challanNo},{stockInViewModel.lcNo},{stockInViewModel.supplierName}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SaveStockInWithOutPO_FromTransferNote(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockInWithOutPO_FromTransferNote {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockDate},{1},{stockInViewModel.remarks},{stockInViewModel.transactionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveRmPmStockInWithOutPO(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetRmPmStockInWithOutPO {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockDate},{1},{stockInViewModel.remarks},{stockInViewModel.transactionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveFactoryFGStockIn(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSaveFactoryFGStockIn {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockDate},{1},{stockInViewModel.remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> UpdateFactoryFGStockIn(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockInQA {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockDate},{1},{stockInViewModel.remarks},{stockInViewModel.transactionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveStockInWithOutPODetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            // await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockInDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetStockInWithOutPODetails {id},{item.stockDetailsId},{stockMasterId},{item.productId},{item.productWiseSpecificationId},{item.stockQty},{item.batchNo},{item.mgfDate},{item.expireDate},{item.purchaseRate}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    return 0;
                }
            }
            return result.isSuccess;
        }
        
        public async Task<int> SaveStockInWithOutPODetails_FromTransferNote(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            // await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockInDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetStockInWithOutPODetails_FromTransferNote {id},{item.stockDetailsId},{stockMasterId},{item.productId},{item.productWiseSpecificationId},{item.stockQty},{item.batchNo},{item.mgfDate},{item.expireDate}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    return 0;
                }
            }
            return result.isSuccess;
        }

        public async Task<int> SaveRmPmStockInWithOutPODetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            // await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockInDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSetRmPmStockInWithOutPODetails {id},{item.stockDetailsId},{stockMasterId},{item.productId},{item.productWiseSpecificationId},{item.stockQty},{item.batchNo},{item.mgfDate},{item.expireDate}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    return 0;
                }
            }
            return result.isSuccess;
        }

        public async Task<int> SaveFactoryFGStockInDetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            // await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockInDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PurSpSaveFactoryFGStockInDetails {id},{item.stockDetailsId},{stockMasterId},{item.productId},{item.productWiseSpecificationId},{item.stockQty},{item.batchNo},{item.mgfDate},{item.expireDate},{item.currentRate}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        public async Task<int> SaveFactoryFGStockInDetailsQA(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            // await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockInDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    if (item.isActive == false)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"InvSpFactoryFGStockInDetailsQA {id},{item.FGStockDetailId}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        #endregion

        #region stock out   --------
        public async Task<JsonViewModel> GetStockOutById(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetStockOutInfoJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetStockDetailsOutById(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockDetailsOutUpdateJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxSRNumber(DateTime SRDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetMaxSRNumberJson {SRDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveStockOut(string id, StockMasterViewModel stockInViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockOut {id}, {stockInViewModel.stockMasterId},{stockInViewModel.companyId},{stockInViewModel.sbuId},{stockInViewModel.storeId},{stockInViewModel.stockNo},{stockInViewModel.stockDate},{2},{stockInViewModel.remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveStockOutDetails(string id, List<StockDetailsViewModel> stockDetailsList, int stockMasterId)
        {
            //await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteStockOutDetails {id},{stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (StockDetailsViewModel item in stockDetailsList)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockOutDetails {id},{item.stockDetailsId},{stockMasterId},{item.poReceiveDetailsId},{item.productId},{item.productWiseSpecificationId},{item.poQty},{item.stockQty},{item.purchaseRate},{item.poReceiveId}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }
            return result.isSuccess;
        }

        public async Task<bool> DeleteStockOutById(string id, int stockMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteStockOut {id}, {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Get current stock Report data ------- 
        public async Task<JsonViewModel> GetCurrentStockReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup, int productTypeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetCurrentStockReportData {productId},{productWiseSpecificationId},{companyId},{sbuId},{storeId},{isStoreWiseGroup},0,{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetAllStockWithoutBatch(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetAllStockWithoutBatch {productId},{productWiseSpecificationId},{companyId},{sbuId},{storeId},{isStoreWiseGroup}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  stock in report -----------

        public async Task<JsonViewModel> GetStockInReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetStockInReportData {productId},{productWiseSpecificationId},{companyId},{sbuId},{storeId},{isStoreWiseGroup}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRepStockInReport(int stockMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetGridStockInReportData {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRepStockInWithOutPoReport(int stockMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetGridStockInWithOutPOReportData {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRptFactoryFGStockIn(int stockMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetFGStockinFactoryProductionReportData {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  stock Out report -----------

        public async Task<JsonViewModel> GetStockOutReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetStockOutReportData {productId},{productWiseSpecificationId},{companyId},{sbuId},{storeId},{isStoreWiseGroup}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRepStockOutReport(int stockMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetGridStockOutReportData {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  stock transfer report ------

        public async Task<JsonViewModel> GetTRNNo(int fromsbuId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferNumberForReportJSON {fromsbuId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetStockTransferReportData(DateTime? fromDate, DateTime? toDate, int? fromSbuId, int? fromStoreId, int? prodTrnfrId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetstoreTransferTRNNumberReportDataJSON {fromDate}, {toDate}, {fromSbuId}, {fromStoreId},{prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region stock receive --------

        public async Task<JsonViewModel> GetProductTransferStockTRNById(int? stockReceiveSbuId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferTRNNoJSON {stockReceiveSbuId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<JsonViewModel> GetMaxProductTransferNumber(DateTime dateTime)
        {
            try
            {
                var res = dateTime.AddDays(9);
                var result = await _context.jsonViewModels.FromSql($"InvSpGetMaxProductStockNumberJson {dateTime}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetStockReceiveById(int? userId, int stockReceiveId, string receiveType, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetStockReceiveJSON {stockReceiveId},{userId},{receiveType},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> SaveStockReceive(string id, StockReceiveViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockTransferReceive {id}, {model.stockReceiveId}, {model.stockReceiveNo},{model.prodTrnfrId},{model.stockReceiveDate},{model.SbuId},{model.purpose},{model.receiveType}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> SaveStockTransferReceiveDetails(string id, List<StockReceiveDetailsViewModel> models, int stockReceiveId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (StockReceiveDetailsViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockTransferReceiveDetails {id},{model.stockReceiveDetailsId},{stockReceiveId},{model.productTrnfrDetailsId},{model.storeId},{model.productId},{model.productWiseSpecificationId},{model.stockReceiveQty},{model.isSelect},{model.batchNo}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteStockTransferReceiveById(int? userId, int masterId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteStockTransferReceiveById {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<JsonViewModel> GetProductTransferStockDetailsById(int? prodReqId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetProductReceiveDetailsForProdStockJSON {prodReqId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetMaxStockTransferNumber(DateTime dateTime, int? userId, string receiveType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetMaxProductStockNumberJson {dateTime},{userId},{receiveType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetStockReceiveIdWiseInUpdate(int? stockReceiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetStockDetailsIdInUpdateJSON {stockReceiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductTransferStockTRNByIdandType(int? stockReceiveSbuId, string type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTransferTRNNoJSON {stockReceiveSbuId},{type}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRptStockReceivePreview(int? stockReceiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpRepGetStockReceiveDataByIdJSON {stockReceiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Stock Transfer Receive  NO--------

        public async Task<JsonViewModel> getSRNo(int sbuId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetStockTansferReceiveNoJSON {sbuId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetAllBatchFromStock()
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetAllBatchNo NULL").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetStockTransferReceiveReportData(int? sbuId, int? storeId, int? stockReceiveId, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetstoctTransferReceiveReportJSON {sbuId}, {storeId},{stockReceiveId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region Miscellaneous Req. & Issue
        //req
        public async Task<JsonViewModel> GetMaterialTypeListForMiscellaneousReq(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaterialTypeListForMiscellaneousReq {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                jv.data = "[]";
                return jv;
            }
        }
        public async Task<JsonViewModel> GetMiscellaneousIssueTypeList(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMiscellaneousIssueTypeList {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                jv.data = "[]";
                return jv;
            }
        }
        public async Task<JsonViewModel> GetPrdRmPmMiscellaneousReq(int? userId, int masterId, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPrdRmPmMiscellaneousReq {userId},{masterId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                jv.data = "[]";
                return jv;
            }
        }
        public async Task<int> SetPrdRmPmMiscellaneousReq(int? userId, PrdRmPmMiscellaneousReqViewModel model)
        {
            try
            {
                var master = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetPrdRmPmMiscellaneousReq {userId},{model.RmPmMiscReqId},{model.RmPmMiscReqNo},{model.RmPmMiscReqDate},{model.productTypeId},{model.miscReqTypeId},{model.reqFrom},{model.reqPurpose},{model.gatePassDate},{model.gatePassNo}").AsNoTracking().FirstOrDefaultAsync();

                foreach (var d in model.lstDetail)
                {
                    var details = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetPrdRmPmMiscellaneousReqDetails {userId},{d.RmPmMiscReqDetailId},{master.isSuccess},{d.productWiseSpecificationId},{d.reqQty},{d.remarks}").AsNoTracking().FirstOrDefaultAsync();
                }

                return master.isSuccess;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<bool> DeletePrdRmPmMiscellaneousReq(int? userId, int masterId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeletePrdRmPmMiscellaneousReq {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePrdRmPmMiscellaneousReqDetails(int? userId, int detailId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeletePrdRmPmMiscellaneousReqDetails {userId}, {detailId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //issue
        public async Task<JsonViewModel> GetPrdRmPmMiscellaneousIssue(int? userId, int masterId, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPrdRmPmMiscellaneousIssue {userId},{masterId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                jv.data = "[]";
                return jv;
            }
        }
        
        public async Task<JsonViewModel> GetPrdRmPmMiscellaneousReqListForIssue(int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetPrdRmPmMiscellaneousReqListForIssue {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                jv.data = "[]";
                return jv;
            }
        }

        public async Task<int> SetPrdRmPmMiscellaneousIssue(int? userId, PrdRmPmMiscellaneousIssueViewModel m)
        {
            try
            {
                var master = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetPrdRmPmMiscellaneousIssue {userId},{m.RmPmMiscIssueId},{m.RmPmMiscIssueNo},{m.RmPmMiscIssueDate},{m.RmPmMiscReqId},{m.issuePurpose},{m.gatePassNo},{m.gatePassDate}").AsNoTracking().FirstOrDefaultAsync();

                foreach (var d in m.lstDetail)
                {
                    var details = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetPrdRmPmMiscellaneousIssueDetails {userId},{d.RmPmMiscIssueDetailId},{master.isSuccess},{d.RmPmMiscReqDetailId},{d.productWiseSpecificationId},{d.IssueQty},{d.batchNo},{d.remarks}").AsNoTracking().FirstOrDefaultAsync();
                }

                return master.isSuccess;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<bool> DeletePrdRmPmMiscellaneousIssue(int? userId, int masterId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeletePrdRmPmMiscellaneousIssue {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Miscellaneous Req. & Issue

    }
}
