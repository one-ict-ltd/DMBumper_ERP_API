using ONEERP.Areas.Inventory.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IProductCategoryService
    {
        #region  Product Category

        Task<int> SaveProductCategory(string id, ProductCategoryViewModel productCategory);       
        Task<JsonViewModel> GetProductCategoryById(int Id);
        Task<JsonViewModel> getProductByCategoryId(int categoryId, int monthId, string year);
        Task<bool> DeleteProductCategoryById(string id, int ProductCategoryId);
        Task<JsonViewModel> getCategorySales();

        #endregion

        #region Product Category Specification
        Task<int> SaveProductCategorySpec(string id, List<ProductCategorySpecificationViewModel> productCategorySpecificationViewModels, int productCategoryId);
        Task<JsonViewModel> GetProductCategorySpecByCategoryId(int productCategoryId);
        Task<JsonViewModel> GetAllProductForRequisition(int productId,int employeeId);
        Task<JsonViewModel> GetAllProductForStockInStockOut(int productId,int employeeId);
        Task<JsonViewModel> GetAllProductForRequisitionByProductTypeId(int productTypeId, int employeeId);
        Task<JsonViewModel> getAllProductForCreditNote(int productId,int employeeId);
        Task<JsonViewModel> GetAllProductForFrizz(int productId,int employeeId);
        Task<JsonViewModel> GetAllProductForDiscountPolicy(int? employeeId, int? withoutDealProduct, DateTime? fromDate, DateTime? toDate, int? partyId);
        Task<JsonViewModel> GetAllProductForAllDiscountPolicy(int? employeeId, string policyType, int? partyId, DateTime? fromDate, DateTime? toDate, bool withoutDealProduct);
        Task<JsonViewModel> GetAllPromoSampleProducts(int productId,int employeeId);
        Task<JsonViewModel> GetAllProductWithCategory(int productId, int employeeId);
        Task<JsonViewModel> GetAllPromotionalItem(int productId, int employeeId);
        Task<JsonViewModel> GetAllProductWithCategoryWithPrice(int productId, int employeeNo, int partyId);


        Task<JsonViewModel> GetTerritoryWisePromotionalItemCS(int productId, int employeeId,string territoryCode);

        #endregion 

        #region  Product Sub Category

        Task<bool> SaveProductSubCategory(string id, ProductSubCategoryViewModel productSubCategoryViewModel);       
        Task<JsonViewModel> GetProductSubCategoryById(int productCategoryId, int productSubCategoryId);
        Task<bool> DeleteProductSubCategoryById(string id, int productSubCategoryId);

        #endregion

        #region  Product Brand

        Task<bool> SaveProductBrand(string id, ProductBrandViewModel productBrandViewModel);
        Task<JsonViewModel> GetProductBrandById(int brandId);
        Task<bool> DeleteProductBrandById(string id, int brandId);

        #endregion

        #region  Product Model

        Task<bool> SaveProductModel(string id, ProductModelViewModel productModelViewModel);
        Task<JsonViewModel> GetProductModelById(int modelId);
        Task<bool> DeleteProductModelById(string id, int modelId);

        #endregion

        #region  Product UOM

        Task<bool> SaveProductUOM(string id, ProductUomViewModel productUomViewModel);
        Task<JsonViewModel> GetProductUOMById(int uomId);
        Task<bool> DeleteProductUOMById(string id, int uomId);

        #endregion

        #region  Product Discount Type

        Task<bool> SaveProductDiscountType(string id, ProductDiscountTypeViewModel productDiscountTypeViewModel);
        Task<JsonViewModel> GetProductDiscountTypeById(int discountTypeId);
        Task<bool> DeleteProductDiscountTypeById(string id, int discountTypeId);

        #endregion

        #region  Product Type

        Task<bool> SaveProductType(string id, ProductTypeViewModel productTypeViewModel);
        Task<JsonViewModel> GetProductTypeById(int companyId, int sbuId,int productTypeId, string flag = null);
        Task<JsonViewModel> getAllProductTypesForUserPermission();
        Task<JsonViewModel> getAllUserForProductPermission(int? companyId);
        Task<bool> DeleteProductTypeById(string id, int productTypeId);

        #endregion

        #region  Product Color

        Task<bool> SaveProductColor(string id, ProductColorViewModel productColorViewModel);

        Task<bool> setUserWiseProductType(string id, UserWiseProductCategoryViewModel model);
        Task<JsonViewModel> GetUserWiseProductType(int? userId, int? userProductTypeId);
        Task<bool> DeleteUserWiseProductType(int? userId, int? userProductTypeId);
        Task<JsonViewModel> GetProductColorById(int colorId);
        Task<bool> DeleteProductColorById(string id, int colorId);

        #endregion

        #region  Product Size

        Task<bool> SaveProductSize(string id, ProductSizeViewModel productSizeViewModel);
        Task<JsonViewModel> GetProductSizeById(int uomId, int sizeId);
        Task<bool> DeleteProductSizeById(string id, int sizeId);

        #endregion

        #region Product Grade
        Task<JsonViewModel> getProductGradeById(int gradeId);
        #endregion

        #region Product Origin Country

        Task<JsonViewModel> getProductOriginCountryById(int countryId);
        Task<JsonViewModel> GetProductSupplier(int partyId);
        #endregion

        #region FinalizeRequisitionProducts
        Task<JsonViewModel> GetAllFinalizeRequisitionProducts(int finalizeDetailId, int employeeId);
        Task<JsonViewModel> GetAllProductForBOM(int productId, int type, int employeeId);
        #endregion

        #region Make Model
        Task<JsonViewModel> GetMakeById(int id);
        Task<JsonViewModel> GetMakeModelByMakeId(int makeId, int makeModelId);
        #endregion
    }
}
