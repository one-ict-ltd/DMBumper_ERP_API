using Microsoft.EntityFrameworkCore;
using ONEERP.Data;
using ONEERP.ERPServices.ExpenseDashboard.Interfaces;
using ONEERP.Models;
using System;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.ExpenseDashboard.Services
{
    public class ExpenseDashboardService : IExpenseDashboardService
    {
        #region Fields

        private readonly ERPDbContext _context;

        #endregion

        #region Ctor

        public ExpenseDashboardService(ERPDbContext context) => _context = context;


        #endregion

        #region Methods


        public async Task<JsonViewModel> GetLocationWiseExpense(int? userId, string locationType, string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate, bool isDetails)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"DashboardSpGetLocationWiseExpense {userId},{locationType},{SetNullIfEmpty(zoneCodes)},{SetNullIfEmpty(regionCodes)},{SetNullIfEmpty(areaCodes)},{SetNullIfEmpty(territoryCodes)},{fromDate},{toDate},{isDetails}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetNationalExpeseSumamry(int? userId,string locationType ,string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"DashboardSpGetNationalExpenseSummary {userId},{SetNullIfEmpty(zoneCodes)},{SetNullIfEmpty(regionCodes)},{SetNullIfEmpty(areaCodes)},{SetNullIfEmpty(territoryCodes)},{fromDate},{toDate},{SetNullIfEmpty(locationType)}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Summary:
        //     Actually sends the data of Cost Head of Expense Dashboard
        //     characters.
        //
        // Parameters:userId, expenseYear
        //   value:
        //     both nullable integer
        //
        // Returns:
        //     Head wise cost accroding to months of given year
        public async Task<JsonViewModel> GetNationalCostHeadWiseExpense(int? userId, int? expenseYear, bool isDetails)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"DashboardSpGetNationalExpensesComparison {userId},{expenseYear}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetNationalExpenseComparisonByYears(int? userId, int? expeseYearOne, int? expenseYearTwo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"DashboardSpGetNationalExpensesComparisonMonthly {userId},{expeseYearOne},{expenseYearTwo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetDepotWiseExpense(int? userId, int? expenseYear, bool isDetails)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"DashboardSpGetDepotWiseWiseExpense {userId},{expenseYear},{isDetails}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> ExpenseCategoryWiseOverview(int? userId, string locationType, string zoneCodes, string regionCodes, string areaCodes, string territoryCodes, DateTime? fromDate, DateTime? toDate, bool isDetails)
        {
            try
            {
                string sql = $"DashboardSpGetExpenseCategoryWiseWiseExpense {userId},{locationType},{SetNullIfEmpty(zoneCodes)},{SetNullIfEmpty(regionCodes)},{SetNullIfEmpty(areaCodes)},{SetNullIfEmpty(territoryCodes)},{fromDate},{toDate},{isDetails}";

                var result = await _context.jsonViewModels.FromSql($"DashboardSpGetExpenseCategoryWiseWiseExpense {userId},{locationType},{SetNullIfEmpty(zoneCodes)},{SetNullIfEmpty(regionCodes)},{SetNullIfEmpty(areaCodes)},{SetNullIfEmpty(territoryCodes)},{fromDate},{toDate},{isDetails}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private string SetNullIfEmpty(string codes)
        {
           return string.IsNullOrWhiteSpace(codes) || codes.Equals("null", StringComparison.InvariantCultureIgnoreCase) || codes == "undefined" ? null : codes;
                
        }
    }
}
