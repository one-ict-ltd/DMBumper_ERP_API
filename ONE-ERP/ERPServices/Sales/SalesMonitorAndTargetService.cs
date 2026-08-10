using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesMonitorAndTargetService : ISalesMonitorAndTargetService
    {
        private readonly ERPDbContext _context;
        public SalesMonitorAndTargetService(ERPDbContext context)
        {
            _context = context;
        }
        #region GeneralCustomerBonusPolicy

        public async Task<int> SaveProductMonitor(int? userId, SalProductMonitorViewModel model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (SalProductMonitorViewModel m in model.lstProductMonitor)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetProductMonitor {userId},{m.monitorId},{m.productWiseSpecificationId},{m.fromDate},{m.toDate},{m.territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> SaveWeeklyTargetPercentage(int? userId, SalWeeklyTargetPercentage model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (SalWeeklyTargetPercentage m in model.lstWeeklyTargetPercentage)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetWeeklyTargetPercentage {userId},{m.weeklyTargetId},{m.startDate},{m.endDate},{m.tgPercent},{m.weekNo}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteProductMonitor(int? userId, int monitorId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteProductMonitor {userId}, {monitorId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteWeeklyTargetPercentage(int? userId, int weeklyTargetId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteWeeklyTargetPercentage {userId}, {weeklyTargetId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductMonitor(int? userId, int? monitorId, DateTime? fromDate, DateTime? toDate, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductMonitor {userId}, {monitorId}, {fromDate}, {toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetWeeklyProductMonitorReport(int? userId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, string empCode)
        {
            var result = await _context.jsonViewModels.FromSql($"salSpGetWeeklyProductMonitorReportJSON {userId}, {fDate}, {tDate}, {zoneCode}, {regionCode}, {areaCode}, {depotCode}, {territoryCode}, {empCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetWeeklyProductTarget(int? userId, DateTime? fDate, DateTime? tDate, int? weeklyTargetId)
        {
            var result = await _context.jsonViewModels.FromSql($"salSpGetWeeklyTargetPercentage {userId}, {fDate}, {tDate}, {weeklyTargetId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetWeeklyTargetPercentageById(int? userId, int? weeklyTargetId)
        {
            var result = await _context.jsonViewModels.FromSql($"salSpGetWeeklyTargetPercentageByIdJSON {userId}, {weeklyTargetId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveMIOSalesForecast(string userId, MIODailySalesForecastViewModel m)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();

                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetMIOSalesForecast {userId},{m.territoryCode},{m.noOfOrder},{m.orderValue},{m.employeeId},{m.orderDate}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveExecutiveWiseProduct(int? userId, List<SalExecutiveWiseProductViewModel> executiveWiseProductViewModels)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (SalExecutiveWiseProductViewModel m in executiveWiseProductViewModels)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetExecutiveWiseProduct {userId},{m.ExecutiveWiseProductId},{m.EmployeeId},{m.ProductId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetExecutiveWiseProduct(int? executiveWiseProductId)
        {
            executiveWiseProductId = executiveWiseProductId ?? 0;
            var result = await _context.jsonViewModels.FromSql($"salSpGetExecutiveWiseProductJSON {executiveWiseProductId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteExecutiveWiseProduct(int? userId, int executiveWiseProductId)
        {
            try
            {
                var result = new SaveUpdateViewModel();
                result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteExecutiveWiseProduct {userId},{executiveWiseProductId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<JsonViewModel> GetProductWiseGrossReturn(int? userId, string depotCode, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql("EXEC spGetProductWiseGrossReturn @p0, @p1, @p2, @p3",
                new object[] { userId, fDate, tDate, string.IsNullOrEmpty(depotCode) ? null : depotCode }).AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion


    }
}
