using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IProductService
    {
        
        #region  Product Category

        Task<int> SaveProduct(string Id, ProductViewModel productCategory);
        Task<int> SaveMaterial(string Id, ProductViewModel productCategory);
        Task<bool> SaveProductDiscount(int productId,string Id, ProductWiseDiscountViewModel productDiscount);
        

        Task<int> SaveProductSupplier(int productId, string Id, List<ProductSupplierViewModel> productSupplier);

        Task<int> setProductWiseSpecification(int productId,string Id,List<ProductWiseSpecificationViewModel> productWiseColor);
        Task<int> setMaterialWiseSpecification(int productId, string Id, List<ProductWiseSpecificationViewModel> productWiseColor);
        Task<int> SaveProductWiseSize(int productId, string Id, List<ProductWiseSizeViewModel> productWiseColor);
        Task<int> SaveProductPricing(int productId, string Id, List<ProductWisePricingViewModel> productPricing);

        Task<JsonViewModel> GetProductJsonById(int productId);
        Task<JsonViewModel> GetMaterialJsonById(int productId, int? userId);
        Task<JsonViewModel> GetLastPurchaseOrderDetailsBySpecId(int productId);
        Task<JsonViewModel> getTypeWiseProducts(int? userId, int productId, int productTypeId, string flag);
        Task<JsonViewModel> getFinishedProducts();
        Task<JsonViewModel> getProductWiseBarCodeJsonById(int productId);
        Task<JsonViewModel> getProductWiseBarCodeInUpdateJsonById(int productId);
        Task<JsonViewModel> getProductWiseSupplierInUpdate(int productId);
        Task<JsonViewModel> getProductWiseSpecificationInUpdateJsonById(int productId);
        Task<JsonViewModel> getDiscountList(int productId);
        Task<JsonViewModel> getProductSupplierIdWise(int supplierId);
        Task<JsonViewModel> getBrandByProductCategory(string Id,int productCategory);
        Task<bool> DeleteProductById(string Id, int ProductId);
        Task<bool> DeleteProductDiscountById(string Id, int discountId);
        Task<JsonViewModel> getProductWiseColor(int productId);
        Task<JsonViewModel> getProductSpecificationInUpdate(int productId);
        Task<JsonViewModel> getProductWiseSize(int productId);
        Task<JsonViewModel> getProductSpecification(int productCategoryId);
        Task<JsonViewModel> getProductImage(string filePath);

        Task<IEnumerable<ProductViewModel>> getProduct();
        Task<IEnumerable<ProductViewModel>> getProduct(int productId);
        Task<IEnumerable<ProductWiseSpecificationViewModel>> getProductSKUNumber();

        #endregion

        #region  Product Set

        Task<int> SaveProductSetMaster(string userId, ProductSetMasterViewModel models);
        Task<int> SaveProductSetDetails(string userId, int productSetMasterId, List<ProductSetDetailsViewModel> models);
        Task<bool> DeleteProductSetMasterByMasterId(string userId, int productSetMasterId);
        Task<bool> DeleteProductSetDetailsById(string userId, int productSetDetailsId);
        Task<JsonViewModel> GetProductSetMasterById(int productSetMasterId);
        Task<JsonViewModel> GetProductSetDetailsById(int productSetMasterId);
        Task<JsonViewModel> GetProductSetReportById(int productSetMasterId);

        #endregion 

        #region  Product Wise Color

        //Task<int> SaveProductWiseColor(int productId, string Id, List<ProductWiseColorViewModel> productWiseColor);
        Task<int> SaveProductWiseColor(string userId, ProductWiseColorViewModel model);
        Task<JsonViewModel> VarifyPromoProductUploadData(string userId, string skuNumber, string productCode);
        Task<int> UploadPromoProduct(string userId, List<PromoProductUploadViewModel> products);
        Task<JsonViewModel> GetProductWiseColorById(int productWiseColorId);
        Task<JsonViewModel> GetAllPromoUploadedProducts(int productId);
        Task<bool> DeleteProductWiseColorById(string userId, int productWiseColorId);
        Task<bool> DeleteProductWiseSpectById(string userId, int spectId);

        #endregion

        #region  Product Spec Info

        Task<bool> SaveProductSpecInfo(string id, ProductSpecInfoViewModel productSpecInfoViewModel);
        Task<JsonViewModel> GetProductSpecInfoById(int productSpecInfoId);
        Task<bool> DeleteProductSpecInfoById(string id, int productSpecInfoId);
        #endregion
    }
}
