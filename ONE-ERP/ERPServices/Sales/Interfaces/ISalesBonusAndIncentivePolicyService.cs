using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesBonusAndIncentivePolicyService
    {
        #region GeneralCustomerBonusPolicy

        Task<int> SaveGeneralCustomerBonusPolicy(string userId, SalGeneralCustomerBonusPolicyViewModel model);
        Task<bool> DeleteGeneralCustomerBonusPolicy(string userId, int generalPolicyId);
        Task<JsonViewModel> GetGeneralCustomerBonusPolicy(int? generalPolicyId);

        #endregion


        #region MangoCustomerBonusPolicy

        Task<int> SaveMangoCustomerBonusPolicy(string userId, SalMangoCustomerBonusPolicyViewModel model);
        Task<bool> DeleteMangoCustomerBonusPolicy(string userId, int mangoPolicyId);
        Task<JsonViewModel> GetMangoCustomerBonusPolicy(int? mangoPolicyId);

        #endregion


        #region ProductSpecWiseIncentivePolicy

        Task<int> SaveProductSpecWiseIncentivePolicy(string userId, List<SalProductSpecWiseIncentivePolicyViewModel> models);
        Task<bool> DeleteProductSpecWiseIncentivePolicy(string userId, int incentivePolicyId);
        Task<JsonViewModel> GetProductSpecWiseIncentivePolicy(int? incentivePolicyId, DateTime? fDate, DateTime? tDate);

        #endregion

        #region Discount Rate Policy
        Task<JsonViewModel> SalSpGetSalesDiscountRatePolicy(int? userid, int? DiscountRateId, string depotCode, int partyId, string discountType, DateTime? fromDate , DateTime? toDate);
        Task<int> SaveDiscountRatePolicy(string userId, SalDiscountRateViewModel model);
        Task<bool> DeleteDiscountRatePolicy(string userId, int DiscountRateId);
        Task<JsonViewModel> SalSpGetitemPriceBySpecId(int? SecId);
        Task<JsonViewModel> SalSpGetDiscountItemPolicy(int? DiscountItemId, DateTime? fromDate, DateTime? endDate, int? userId);
        Task<JsonViewModel> GetProductsForDiscount(int? productTypeId);
        Task<int> SaveDiscountItemPolicy(string userId, SalDiscountItemViewModel model);
        Task<int> SaveListOfDiscountItemPolicy(string userId, SalDiscountRateViewModel model);
        Task<bool> DeleteDiscountItemPolicy(string userId, int DiscountItemId);
        #endregion

        Task<int> SaveCategorySalesMaster(string userId, int? month, string year, int? productCategoryId);
        Task<int> SaveCategorySalesMasterDetails(string userId, List<SalesCategoryWiseProductDetailsVM> models, int masterId);
        Task<int> UpdateStatusOfDiscountItemPolicies(int? userId, List<SalDiscountPolicyUpdateViewModel> models);
    }
}
