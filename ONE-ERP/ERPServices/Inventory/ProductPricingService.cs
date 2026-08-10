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
    public class ProductPricingService : IProductPricingService
    {
        private readonly ERPDbContext _context;
        public ProductPricingService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetProductPricingByMasterId(int? pricingId, int? productWiseSpecificationId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvGetProductPricingJSON {pricingId}, {productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveProductPricing(string userId, ProductPricingViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductPricingNew {userId}, {model.pricingId}, {model.productId}, {model.productWiseSpecificationId}, {model.effectiveDate}, {model.price}, {model.barcodeNo}, {model.barcodeId}, {model.avgPurchasePrice}, {model.minimumSalePrice},{model.tradePrice},{model.unitVat}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }
        
        public async Task<int> SaveCashSetUp(string userId, CashSetUpViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"HrmSpSetCashSalarySetup {userId}, {model.employeeId}, {model.cashAmount},{model.walletAmount},{model.defaultAmount}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductPricingNByMasterId(int? pricingId, int? productWiseSpecificationId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvGetProductPricingNJSON {pricingId}, {productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> GetEmployeeCashSalaryJSON(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"HRGetEmployeeCashSalaryJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
