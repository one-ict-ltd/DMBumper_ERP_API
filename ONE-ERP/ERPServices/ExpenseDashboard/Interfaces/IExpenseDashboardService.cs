using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.ExpenseDashboard.Interfaces
{
    public interface IExpenseDashboardService
    {
        Task<JsonViewModel> GetLocationWiseExpense(int? userId, string locationType, string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate, bool isDetails);
        Task<JsonViewModel> ExpenseCategoryWiseOverview(int? userId, string locationType, string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate, bool isDetails);
        Task<JsonViewModel> GetNationalExpeseSumamry(int? userId, string locationType, string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate);
        Task<JsonViewModel> GetNationalCostHeadWiseExpense(int? userId, int? expenseYear, bool isDetails);
        Task<JsonViewModel> GetDepotWiseExpense(int? userId, int? expenseYear, bool isDetails);
        Task<JsonViewModel> GetNationalExpenseComparisonByYears(int? userId, int? expeseYearOne, int? expenseYearTwo);
    }
}
