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
    public class StockInWithBarcodeService : IStockInWithBarcodeService
    {
        private readonly ERPDbContext _context;
        public StockInWithBarcodeService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<JsonViewModel> GetMaxBarcodeNo(DateTime? date)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"InvSpGetMaxBarcodeNumber {date}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<JsonViewModel> GetStockInWithBarcodeById(int? barcodeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockInWithBarcode {barcodeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetStockInWithBarcodeDetailsById(int? barcodeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockInWithBarcodeDetails {barcodeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveStockInWithBarcode(string id, List<StockInWithBarcode> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {

                foreach (var model in models)
                {
                    //string s = $"InvSpSetStockInWithBarcode {id},{model.barcodeId},{model.barcodeNo},{model.stockInDate},{model.storeId},{model.productWiseSpecificationId},{model.receiveQty},{model.remarks},{model.isActive},{model.isSelect}, {model.hasSerial}";

                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockInWithBarcode {id},{model.barcodeId},{model.barcodeNo},{model.stockInDate},{model.storeId},{model.productWiseSpecificationId},{model.receiveQty},{model.remarks},{model.isActive},{model.isSelect}, {model.hasSerial}, {model.partyId}, {model.purchasePrice}").AsNoTracking().FirstOrDefaultAsync();

                    if (model.hasSerial && result.isSuccess != 0)
                    {
                        //var detailsRes = SaveStockInWithBarcodeDetails(id, result.isSuccess, model.lstDetailsViewModel);
                        var result2 = new SaveUpdateValueViewModel();
                        foreach (var item in model.lstDetailsViewModel)
                        {
                            result2 = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockInWithBarcodeDetails {id},{item.barcodeDetailsId},{result.isSuccess},{item.serialNo},{item.isActive},{item.isSale}").AsNoTracking().FirstOrDefaultAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result.isSuccess;
        }
        public async Task<int> SaveStockInWithBarcodeDetails(string userId, int barcodeId, List<StockInWithBarcodeDetails> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {

                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetStockInWithBarcodeDetails {userId},{model.barcodeDetailsId},{barcodeId},{model.serialNo},{model.isActive},{model.isSale}").AsNoTracking().FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result.isSuccess;
        }
        public async Task<bool> DeleteStockInWithBarcodeById(string id, int barcodeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteStockInWithBarcode {id}, {barcodeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetStockInWithBarcodeReportData(DateTime? fromDate, DateTime? toDate, int? barcodeId, int? fromStoreId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpStockInWithBarcodeReportDataJSON {fromDate}, {toDate}, {barcodeId}, {fromStoreId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetStockInDetailsReportData(string searchingText, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetStockInDetailsReportDataJSON {searchingText}, {fDate}, {tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllPartyByType(int PartyType)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllPartyByTypeJSON {PartyType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
