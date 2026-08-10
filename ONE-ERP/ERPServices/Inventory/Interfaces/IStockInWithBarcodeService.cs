using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IStockInWithBarcodeService
    {
        Task<JsonViewModel> GetStockInWithBarcodeById(int? barcodeId);
        Task<JsonViewModel> GetStockInWithBarcodeDetailsById(int? barcodeId);
        Task<int> SaveStockInWithBarcode(string id, List<StockInWithBarcode> models);
        Task<bool> DeleteStockInWithBarcodeById(string id, int barcodeId);
        Task<JsonViewModel> GetMaxBarcodeNo(DateTime? date);
        Task<JsonViewModel> GetStockInWithBarcodeReportData(DateTime? fromDate, DateTime? toDate, int? barcodeId, int? fromStoreId);
        Task<JsonViewModel> GetStockInDetailsReportData(string searchingText, DateTime? fDate, DateTime? tDate);
        Task<JsonViewModel> GetAllPartyByType(int PartyType);
    }
}
