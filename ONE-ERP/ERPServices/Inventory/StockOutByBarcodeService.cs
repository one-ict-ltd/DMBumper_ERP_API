using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class StockOutByBarcodeService : IStockOutByBarcodeService
    {
        private readonly ERPDbContext _context;
        public StockOutByBarcodeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<JsonViewModel> GetBarcodeDetails(string barcodeNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetBarcodeDetailsJSON {barcodeNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetStockOutByBarcodeByMasterId(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockOutByBarcodeJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetStockOutByBarcodeDetailsByMasterId(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockOutByBarcodeDetailsJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveStockOutByBarcode(string id, StockOutByBarcode model)
        {
            var result = new SaveUpdateValueViewModel();
            result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockOutByBarcode {id}, {model.stockMasterId},{model.companyId},{model.sbuId},{model.storeId},{model.stockNo},{model.stockDate},{model.stockTypeId},{model.stockCategoryId},{model.remarks},{model.transactionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveStockOutByBarcodeDetails(string id, List<StockOutByBarcodeDetails> models, int stockMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (StockOutByBarcodeDetails model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockOutDetailsByBarcode {id},{model.stockDetailsId},{stockMasterId},{model.poReceiveDetailsId},{model.productId},{model.productWiseSpecificationId},{model.poQty},{model.stockQty},{model.purchaseRate},{model.poReceiveId},{model.transactionDetailsId}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
            }
            return result.isSuccess;
        }
        public async Task<bool> DeleteStockOutByBarcodeByMasterId(string id, int stockMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteStockInWithBarcode {id}, {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetMaxStockOutNo(DateTime? date)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetMaxStockOutNumber {date}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public async Task<JsonViewModel> GetStockInWithBarcodeReportData(DateTime? fromDate, DateTime? toDate, int? barcodeId, int? fromStoreId)
        //{
        //    var result = await context.jsonViewModels.FromSql($"InvSpStockInWithBarcodeReportDataJSON {fromDate}, {toDate}, {barcodeId}, {fromStoreId}").AsNoTracking().FirstOrDefaultAsync();
        //    return result;
        //}
    }
}
