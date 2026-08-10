using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ERPDbContext _context;

        public ProductCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        #region  Product Category

        public async Task<int> SaveProductCategory(string id, ProductCategoryViewModel productCategory)
        {           
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductCategory {id},{productCategory.productCategoryId},{productCategory.categoryName},{productCategory.aliasName},{productCategory.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
       
        public async Task<JsonViewModel> GetProductCategoryById(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductCategoryJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getCategorySales()
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetCategorySalesJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductByCategoryId(int categoryId, int monthId, string year)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductByCategoryIdJson {categoryId},{monthId},{year}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductCategoryById(string id, int ProductCategoryId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductCategory {id},{ProductCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Product Category Specification

        public async Task<int> SaveProductCategorySpec(string id, List<ProductCategorySpecificationViewModel> models, int productCategoryId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteProductCategorySpec {id},{productCategoryId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (ProductCategorySpecificationViewModel model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetProductCategorySpec {id},{0},{productCategoryId},{model.specificationType},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductCategorySpecByCategoryId(int productCategoryId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetProductCategorySpecJson {productCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductForRequisition(int productId,int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductForRequisition {productId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductForStockInStockOut(int productId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductForStockInStockOut {productId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductForRequisitionByProductTypeId(int productTypeId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductForRequisitionByProductTypeId {productTypeId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getAllProductForCreditNote(int productId,int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductForReditNote {productId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllProductForFrizz(int productId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductForFrizz {productId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductForDiscountPolicy(int? employeeId, int? withoutDealProduct, DateTime? fromDate, DateTime? toDate, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllProductForDiscountPolicy {employeeId},{withoutDealProduct},{fromDate},{toDate},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        //for all types of discount policies
        public async Task<JsonViewModel> GetAllProductForAllDiscountPolicy(int? employeeId, string policyType, int? partyId, DateTime? fromDate, DateTime? toDate, bool withoutDealProduct)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllProductForAllDiscountPolicy {employeeId},{partyId},{fromDate},{toDate},{withoutDealProduct},{policyType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllPromoSampleProducts(int productId,int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllPromoSampleProducts {productId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductWithCategory(int productId,int employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductWithCategory {productId},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllProductWithCategoryWithPrice(int productId,int employeeNo,int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductWithCategoryWithPrice {productId},{employeeNo},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllPromotionalItem(int productId, int employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllPromotionalItem {productId},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<JsonViewModel> GetTerritoryWisePromotionalItemCS(int productId, int employeeId,string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"PromoSpGetAllPromotionalItem {productId},{employeeId},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion  

        #region  Product Sub Category

        public async Task<bool> SaveProductSubCategory(string id, ProductSubCategoryViewModel productSubCategoryViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductSubCategory {id},{productSubCategoryViewModel.productSubCategoryId},{productSubCategoryViewModel.productCategoryId},{productSubCategoryViewModel.subCategoryName},{productSubCategoryViewModel.parentId},{productSubCategoryViewModel.aliasName},{productSubCategoryViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductSubCategoryById(int productCategoryId, int productSubCategoryId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSubCategoryJson {productCategoryId},{productSubCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteProductSubCategoryById(string id, int productSubCategoryId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductSubCategory {id},{productSubCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Brand

        public async Task<bool> SaveProductBrand(string id, ProductBrandViewModel productBrandViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductBrand {id},{productBrandViewModel.brandId},{productBrandViewModel.brandName},{productBrandViewModel.brandCode},{productBrandViewModel.aliasName},{productBrandViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductBrandById(int brandId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductBrandJson {brandId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductBrandById(string id, int brandId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductBrand {id},{brandId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Model

        public async Task<bool> SaveProductModel(string id, ProductModelViewModel productModelViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductModel {id},{productModelViewModel.modelId},{productModelViewModel.modelName},{productModelViewModel.modelCode},{productModelViewModel.aliasName},{productModelViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductModelById(int modelId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductModelJson {modelId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductModelById(string id, int modelId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductModel {id},{modelId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product UOM

        public async Task<bool> SaveProductUOM(string id, ProductUomViewModel productUomViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductUOM {id},{productUomViewModel.uomId},{productUomViewModel.uomName},{productUomViewModel.uomfullName},{productUomViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductUOMById(int uomId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductUOMJson {uomId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductUOMById(string id, int uomId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductUOM {id},{uomId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Discount Type

        public async Task<bool> SaveProductDiscountType(string id, ProductDiscountTypeViewModel productDiscountTypeViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductDiscountType {id},{productDiscountTypeViewModel.discountTypeId},{productDiscountTypeViewModel.discountTypeName},{productDiscountTypeViewModel.discountTypeCode},{productDiscountTypeViewModel.aliasName},{productDiscountTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductDiscountTypeById(int discountTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductDiscountTypeJson {discountTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductDiscountTypeById(string id, int discountTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductDiscountType {id},{discountTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Type

        public async Task<bool> SaveProductType(string id, ProductTypeViewModel productTypeViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductType {id},{productTypeViewModel.productTypeId},{productTypeViewModel.productTypeName},{productTypeViewModel.sbuId},{productTypeViewModel.companyId},{productTypeViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductTypeById(int companyId, int sbuId, int productTypeId, string flag = null)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductTypeJson {companyId}, {sbuId}, {productTypeId}, {flag}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getAllProductTypesForUserPermission()
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetProductAllTypes").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getAllUserForProductPermission(int? companyId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetAllActiveEmployees {companyId ?? 0}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductTypeById(string id, int productTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductType {id},{productTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Color

        public async Task<bool> SaveProductColor(string id, ProductColorViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductColor {id},{model.colorId},{model.colorName},{model.colorCode},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> setUserWiseProductType(string id, UserWiseProductCategoryViewModel model)
        {
            try
            {
                bool isSuccess = false;
                foreach (var item in model.listViewModel.Where(x => x.isSelect == true))
                {
                    var result = await _context.saveUpdateViewModels.FromSql($"PurSpSetUserWiseProductType {id},{item.userProductTypeId},{item.productTypeId},{model.employeeId}").AsNoTracking().FirstOrDefaultAsync();

                    isSuccess = result.isSuccess;
                }

                return isSuccess;
            }
            catch (Exception ex)
            {

                return false;
            }
            
        }

        public async Task<JsonViewModel> GetUserWiseProductType(int? userId, int? userProductTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"PurSpGetUserWiseProductType {userId}, {userProductTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteUserWiseProductType(int? userId, int? userProductTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PurSpDeleteUserWiseProductType {userId}, {userProductTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductColorById(int colorId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductColorJson {colorId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductColorById(string id, int colorId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductColor {id},{colorId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Size

        public async Task<bool> SaveProductSize(string id, ProductSizeViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductSize {id},{model.sizeId},{model.size},{model.uomId},{model.uomName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductSizeById(int uomId, int sizeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSizeJson {uomId},{sizeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductSizeById(string id, int sizeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductSize {id},{sizeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Product Grade
        public async Task<JsonViewModel> getProductGradeById(int gradeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetProductGrade {gradeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Product Origin Country
        public async Task<JsonViewModel> getProductOriginCountryById(int countryId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetOriginCountry {countryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Product Supplier
        public async Task<JsonViewModel> GetProductSupplier(int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetSupplier").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion
        public async Task<JsonViewModel> GetAllFinalizeRequisitionProducts(int finalizeDetailId, int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PurSpGetAllFinalizeRequisitionProducts {finalizeDetailId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        public async Task<JsonViewModel> GetAllProductForBOM(int productId, int type, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllPromotionalItemforBom {productId},{employeeId},{type}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

    }
}
