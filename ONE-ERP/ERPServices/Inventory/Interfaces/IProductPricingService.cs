using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IProductPricingService
    {
        Task<JsonViewModel> GetProductPricingByMasterId(int? pricingId, int? productWiseSpecificationId);
        Task<int> SaveProductPricing(string userId, ProductPricingViewModel model);
        Task<JsonViewModel> GetProductPricingNByMasterId(int? pricingId, int? productWiseSpecificationId);
        Task<JsonViewModel> GetEmployeeCashSalaryJSON(int? userId);
        Task<int> SaveCashSetUp(string userId, CashSetUpViewModel model);
    }
}
