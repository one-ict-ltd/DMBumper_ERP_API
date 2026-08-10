using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class CurrencyService: ICurrencyService
    {
        private readonly ERPDbContext _context;

        public CurrencyService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveCurrency(string Id, CurrencyViewModel currencyViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetCurrency {Id},{currencyViewModel.currencyId},{currencyViewModel.currencyName},{currencyViewModel.aliasName},{currencyViewModel.conversionRate},{currencyViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<CurrencyListViewModel>> GetCurrency()
        {
            var result = await _context.currencyListViewModels.FromSql($"AccSpGetCurrency {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<CurrencyListViewModel> GetCurrencyById(int id)
        {
            var result = await _context.currencyListViewModels.FromSql($"AccSpGetCurrency {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetCurrencyByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetCurrencyJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDuplicateCurrency(int currencyId, string currencyName)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetDuplicateCurrency {currencyId},{currencyName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteCurrencyById(string Id, int currencyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCurrency {Id},{currencyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetAllActiveInActiveCurrency(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetAllActiveInActiveCurrency {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
