using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesMonitorAndTargetService
    {
        #region GeneralCustomerBonusPolicy

        Task<int> SaveProductMonitor(int? userId, SalProductMonitorViewModel model);
        Task<int> SaveMIOSalesForecast(string userId, MIODailySalesForecastViewModel model);
        Task<int> SaveWeeklyTargetPercentage(int? userId, SalWeeklyTargetPercentage model);
        Task<bool> DeleteProductMonitor(int? userId, int monitorId);
        Task<bool> DeleteWeeklyTargetPercentage(int? userId, int weeklyTargetId);
        Task<JsonViewModel> GetProductMonitor(int? userId, int? monitorId, DateTime? fromDate, DateTime? toDate, string territoryCode);
        Task<JsonViewModel> GetWeeklyProductTarget(int? userId, DateTime? fDate, DateTime? tDate, int? weeklyTargetId);
        Task<JsonViewModel> GetWeeklyTargetPercentageById(int? userId, int? weeklyTargetId);
        Task<JsonViewModel> GetWeeklyProductMonitorReport(int? userId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, string empCode);
        Task<int> SaveExecutiveWiseProduct(int? userId, List<SalExecutiveWiseProductViewModel> executiveWiseProductViewModels);
        Task<JsonViewModel> GetExecutiveWiseProduct(int? executiveWiseProductId);
        Task<bool> DeleteExecutiveWiseProduct(int? userId, int executiveWiseProductId);
        Task<JsonViewModel> GetProductWiseGrossReturn(int? userId, string depotCode, DateTime? fDate, DateTime? tDate);

        #endregion
    }
}
