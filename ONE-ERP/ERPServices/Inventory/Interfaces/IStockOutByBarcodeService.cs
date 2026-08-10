using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IStockOutByBarcodeService
    {
        Task<JsonViewModel> GetStockOutByBarcodeByMasterId(int? stockMasterId);
        Task<JsonViewModel> GetStockOutByBarcodeDetailsByMasterId(int? stockMasterId);
        Task<JsonViewModel> GetBarcodeDetails(string barcodeNo);
        Task<int> SaveStockOutByBarcode(string id, StockOutByBarcode model);
        Task<int> SaveStockOutByBarcodeDetails(string id, List<StockOutByBarcodeDetails> models, int stockMasterId);
        Task<bool> DeleteStockOutByBarcodeByMasterId(string id, int stockMasterId);
        Task<JsonViewModel> GetMaxStockOutNo(DateTime? date);
        //Task<JsonViewModel> GetStockInWithBarcodeReportData(DateTime? fromDate, DateTime? toDate, int? barcodeId, int? fromStoreId);
    }
}
