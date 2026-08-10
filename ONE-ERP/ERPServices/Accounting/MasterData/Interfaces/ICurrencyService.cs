
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData.Interfaces
{
    public interface ICurrencyService
    {
        Task<bool> SaveCurrency(string Id, CurrencyViewModel currencyViewModel);
        Task<IEnumerable<CurrencyListViewModel>> GetCurrency();
        Task<CurrencyListViewModel> GetCurrencyById(int id);
        Task<JsonViewModel> GetCurrencyByIdJson(int id);
        Task<JsonViewModel> GetDuplicateCurrency(int currencyId, string currencyName);
        Task<bool> DeleteCurrencyById(string Id, int currencyId);
        Task<JsonViewModel> GetAllActiveInActiveCurrency(int id);

    }
}
