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
    public class SalesBonusAndIncentivePolicyService : ISalesBonusAndIncentivePolicyService
    {
        private readonly ERPDbContext _context;
        public SalesBonusAndIncentivePolicyService(ERPDbContext context)
        {
            _context = context;
        }
        #region GeneralCustomerBonusPolicy

        public async Task<int> SaveGeneralCustomerBonusPolicy(string userId, SalGeneralCustomerBonusPolicyViewModel model)
        {
            //var result = new SaveUpdateValueViewModel();
            try
            {
                //foreach (var model in models)
                //{
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetGeneralCustomerBonusPolicy {userId}, {model.generalPolicyId}, {model.fromDays}, {model.toDays}, {model.maxDays}, {model.percentValue}, {model.isActive},{model.companyId}").AsNoTracking().FirstOrDefaultAsync();
                //}
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteGeneralCustomerBonusPolicy(string userId, int generalPolicyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteGeneralCustomerBonusPolicy {userId}, {generalPolicyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetGeneralCustomerBonusPolicy(int? generalPolicyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetGeneralCustomerBonusPolicy {generalPolicyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion


        #region MangoCustomerBonusPolicy

        public async Task<int> SaveMangoCustomerBonusPolicy(string userId, SalMangoCustomerBonusPolicyViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetMangoCustomerBonusPolicy {userId}, {model.mangoPolicyId}, {model.fromMonth}, {model.toMonth}, {model.paymentDate}, {model.percentValue}, {model.isActive}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteMangoCustomerBonusPolicy(string userId, int mangoPolicyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteMangoCustomerBonusPolicy {userId}, {mangoPolicyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetMangoCustomerBonusPolicy(int? mangoPolicyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMangoCustomerBonusPolicy {mangoPolicyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion


        #region ProductSpecWiseIncentivePolicy

        public async Task<int> SaveProductSpecWiseIncentivePolicy(string userId, List<SalProductSpecWiseIncentivePolicyViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetProductSpecWiseIncentivePolicy {userId},{model.incentivePolicyId}, {model.incentiveType},{model.incentiveValue},{model.effectiveDate},{model.productWiseSpecificationId},{model.minOrderQty},{model.uom},{model.isActive},{model.toDate},{model.collUpToDays}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteProductSpecWiseIncentivePolicy(string userId, int incentivePolicyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteProductSpecWiseIncentivePolicy {userId}, {incentivePolicyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductSpecWiseIncentivePolicy(int? incentivePolicyId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductSpecWiseIncentivePolicy {incentivePolicyId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Discount Rate Policy
        public async Task<JsonViewModel> SalSpGetSalesDiscountRatePolicy(int? userid, int? DiscountRateId, string depotCode, int partyId, string discountType, DateTime? fromDate, DateTime? toDate)
        {

            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDiscountRatePolicy {DiscountRateId}, {userid}, {depotCode},{partyId}, {discountType}, {fromDate}, {toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        
        public async Task<JsonViewModel> SalSpGetDiscountItemPolicy(int? DiscountItemId, DateTime? fromDate, DateTime? endDate, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDiscountItemPolicy {DiscountItemId},{fromDate},{endDate},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductsForDiscount(int? productTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductForDiscount {productTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> SalSpGetitemPriceBySpecId(int? SecId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetitemPriceBySpecId {SecId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveDiscountRatePolicy(string userId, SalDiscountRateViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetDiscountRatePolicy {userId}, {model.DiscountRateId}, {model.productWiseSpecificationId}, {model.partyId}, {model.price}, {model.discountType}, {model.percentAmount}, {model.discountAmount}, {model.amount}, {model.fromDate}, {model.endDate},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveDiscountItemPolicy(string userId, SalDiscountItemViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetDiscountItemPolicy {userId}, {model.DiscountItemId}, {model.bonusforSpecificationId}, {model.partyId}, {model.forQuantity}, {model.bonusSpecificationId}, {model.quantity}, {model.fromDate}, {model.endDate},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> SaveListOfDiscountItemPolicy(string userId, SalDiscountRateViewModel model)
        {
            
            try
            {
                foreach(var item in model.selectedProductList)
                {
                    //var  discountAmount = (item.price * (model.percentAmount / 100));
                    var  discountAmount = (item.price * (item.percentAmount / 100));
                    var amount = item.price - discountAmount;

                    var sql = $"SalSpSetDiscountRatePolicy {userId}, {model.DiscountRateId}, {item.productWiseSpecificationId}, {model.partyId}, {item.price}, {model.discountType}, {model.percentAmount}, {discountAmount}, {amount}, {model.fromDate}, {model.endDate},{model.isActive}";

                    //var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetDiscountRatePolicy {userId}, {model.DiscountRateId}, {item.productWiseSpecificationId}, {model.partyId}, {item.price}, {model.discountType}, {model.percentAmount}, {discountAmount}, {amount}, {model.fromDate}, {model.endDate},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();

                    var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetDiscountRatePolicy {userId}, {model.DiscountRateId}, {item.productWiseSpecificationId}, {model.partyId}, {item.price}, {item.discountType}, {item.percentAmount}, {discountAmount}, {amount}, {model.fromDate}, {model.endDate},{item.isActive}").AsNoTracking().FirstOrDefaultAsync();
                }
                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteDiscountRatePolicy(string userId, int DiscountRateId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteDiscountRatePolicy {userId}, {DiscountRateId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteDiscountItemPolicy(string userId, int DiscountItemId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteDiscountItemPolicy {userId}, {DiscountItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        public async Task<int> SaveCategorySalesMaster(string userId, int? month, string year, int? productCategoryId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetSaveCategorySalesMaster {userId}, {month},{year}, {productCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveCategorySalesMasterDetails(string userId, List<SalesCategoryWiseProductDetailsVM>models, int masterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (SalesCategoryWiseProductDetailsVM model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SpSetSaveCategorySalesDetails {userId},{model.productId},{model.isChecked},{masterId},{model.salesCategoryWiseProductDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<int> UpdateStatusOfDiscountItemPolicies(int? userId, List<SalDiscountPolicyUpdateViewModel> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (SalDiscountPolicyUpdateViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpUpdateStatusOfDiscountPolicies {userId},{model.tableName},{model.tableId},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
