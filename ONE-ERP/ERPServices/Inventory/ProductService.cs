using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class ProductService : IProductService
    {
        private readonly ERPDbContext _context;

        public ProductService(ERPDbContext context)
        {
            _context = context;
        }

        #region  Product 
        public async Task<int> SaveProduct(string Id, ProductViewModel product)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProduct {Id},{product.productId},{product.productCode},{product.productName},{product.width},{product.height},{product.weight},{product.isQCRequired},{product.hsCODE},{product.description},{product.warrantyDuration},{product.notificationDay},{product.productTypeId},{product.productCategoryId},{product.productSubCategoryId},{product.modelId},{product.brandId},{product.uomId},{product.originCountryId},{product.gradeId},{product.companyId},{product.isActive},{product.expiryDate}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        public async Task<int> SaveMaterial(string Id, ProductViewModel product)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetMaterial {Id},{product.productId},{product.productCode},{product.productName},{product.width},{product.height},{product.weight},{product.isQCRequired},{product.hsCODE},{product.description},{product.warrantyDuration},{product.notificationDay},{product.productTypeId},{product.productCategoryId},{product.productSubCategoryId},{product.modelId},{product.brandId},{product.uomId},{product.originCountryId},{product.gradeId},{product.companyId},{product.isActive},{product.expiryDate}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }



        public async Task<int> SaveProductSupplier(int productId, string id, List<ProductSupplierViewModel> productWiseSupplier)
        {
            try
            {
                await _context.saveUpdateViewModels.FromSql($"InvDeleteProductWiseSupplier {id},{productId}").AsNoTracking().FirstOrDefaultAsync();
                var result = new SaveUpdateValueViewModel();

                foreach (ProductSupplierViewModel productwiseListViewModel in productWiseSupplier)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSupplier {id},{productId},{productwiseListViewModel.supplierId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        //public async Task<int> setProductWiseSpecification(int productId, string id, List<ProductWiseSpecificationViewModel> productWiseColor)
        //{
        //    try
        //    {
        //        var result = new SaveUpdateValueViewModel();
        //        var NewSKU = "";
        //        foreach (ProductWiseSpecificationViewModel model in productWiseColor)
        //        {
        //            if (model.skuNumber != NewSKU)
        //            {
        //                string[] res = model.imageFile.Split(',');
        //                if (string.IsNullOrEmpty(model.imageUrl) && res.Length > 1)
        //                {
        //                    Byte[] bytes = Convert.FromBase64String(res[1]);

        //                    string[] extention = res[0].Split("/");
        //                    string servePath = ("./wwwroot/ProductImages");
        //                    if (!System.IO.Directory.Exists(servePath)) System.IO.Directory.CreateDirectory(servePath);
        //                    string fileName = ($"{DateTime.Now.Ticks}.{extention[1].Replace(";base64", "")}");
        //                    string filePath = ($"{servePath}/{fileName}");
        //                    File.WriteAllBytes(filePath, bytes);

        //                    model.imageUrl = filePath;//fileName
        //                }

        //                model.productId = model.productId == 0 ? productId : model.productId;

        //                result = await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSpecification {id},{model.productWiseSpecificationId},{model.productId},{model.skuName},{model.skuNumber},{model.imageUrl}").AsNoTracking().FirstOrDefaultAsync();
        //                if (model.value != "")
        //                {
        //                    await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSpecificationDetails {id},{model.specificationDetailsId},{result.isSuccess},{model.productCategorySpecificationId},{model.value}").AsNoTracking().FirstOrDefaultAsync();
        //                }

        //            }
        //            else
        //            {
        //                if (model.value != "")
        //                {
        //                    await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSpecificationDetails {id},{model.specificationDetailsId},{result.isSuccess},{model.productCategorySpecificationId},{model.value}").AsNoTracking().FirstOrDefaultAsync();
        //                }
        //            }
        //            NewSKU = model.skuNumber;
        //        }
        //        return result.isSuccess;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<int> setProductWiseSpecification(int productId, string id, ProductViewModel model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                model.productId = model.productId == 0 ? productId : model.productId;

                //result = await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSpecification {id},{model.productWiseSpecificationId},{model.productId},{model.skuName},{model.skuNumber},{model.imageUrl}").AsNoTracking().FirstOrDefaultAsync();
                result = await _context.saveUpdateValueViewModels
                        .FromSql($@"EXEC InvSetProductWiseSpecification
                            {id},
                            {model.productWiseSpecificationId},
                            {model.productId},
                            {model.skuName},
                            {model.skuNumber},
                            {model.imageUrl},

                            {model.partslink},
                            {model.location},
                            {model.qtyonHand},
                            {model.uom},
                            {model.listPrice},
                            {model.costPrice},
                            {model.salesPrice},
                            {model.fromYear},
                            {model.toYear},
                            {model.make},
                            {model.model},
                            {model.category},
                            {model.subCategory},
                            {model.oem},
                            {model.interchange},
                            {model.patent},
                            {model.side},
                            {model.position},
                            {model.material},
                            {model.colorOrFinish},
                            {model.certification},
                            {model.status},
                            {model.barcodeOrQR},
                            {model.productWeight},
                            {model.productWeight_UOM},
                            {model.productWidth},
                            {model.productHeight},
                            {model.productLength},
                            {model.productSizeUOM},
                            {model.productActive},
                            {model.productTaxable},
                            {model.isWebsiteActive},
                            {model.isReturnable},
                            {model.warrantyDays},
                            {model.lastReceivedDate},
                            {model.lastSoldDate},
                            {model.primaryVendor},
                            {model.vendorType},
                            {model.submodelOrTrim},
                            {model.bodyStyle},
                            {model.engineSize},
                            {model.warehouse},
                            {model.zone},
                            {model.aisle},
                            {model.rack},
                            {model.shelf},
                            {model.bin},
                            {model.pickLocationOrZone},
                            {model.bulkLocationOrZone},
                            {model.qtyReserved},
                            {model.qtyDamagedHold},
                            {model.qtyReceivingHold},
                            {model.qtyReturnIntake},
                            {model.qtyVendorReturn},
                            {model.qtyScrap},
                            {model.previousCountdays},
                            {model.spotCountDate},
                            {model.currentCountDays},
                            {model.cycleCountFrequency},
                            {model.abc_Class},
                            {model.leadTimeDays},
                            {model.safetyStock},
                            {model.minStock},
                            {model.maxStock},
                            {model.suggestedReorderQty},
                            {model.vendorName},
                            {model.defaultVendor},
                            {model.vendorPartNumber},
                            {model.cost},
                            {model.vendorUOM},
                            {model.assetAccount},
                            {model.cogsAccount},
                            {model.adjustmentAccount},
                            {model.scrapAccount},
                            {model.varianceAccount},
                            {model.incomeAccount},
                            {model.upc},
                            {model.partTypeID},
                            {model.batchNumber},
                            {model.notes},

                            {model.uomId},
                            {model.makeId},
                            {model.makeModelId},
                            {model.productCategoryId},
                            {model.productSubCategoryId}")
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> setMaterialWiseSpecification(int productId, string id, List<ProductWiseSpecificationViewModel> productWiseColor)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                var NewSKU = "";
                foreach (ProductWiseSpecificationViewModel model in productWiseColor)
                {
                    if (model.skuNumber != NewSKU)
                    {
                        string[] res = model.imageFile.Split(',');
                        if (string.IsNullOrEmpty(model.imageUrl) && res.Length > 1)
                        {
                            Byte[] bytes = Convert.FromBase64String(res[1]);

                            string[] extention = res[0].Split("/");
                            string servePath = ("./wwwroot/ProductImages");
                            if (!System.IO.Directory.Exists(servePath)) System.IO.Directory.CreateDirectory(servePath);
                            string fileName = ($"{DateTime.Now.Ticks}.{extention[1].Replace(";base64", "")}");
                            string filePath = ($"{servePath}/{fileName}");
                            File.WriteAllBytes(filePath, bytes);

                            model.imageUrl = filePath;//fileName
                        }

                        model.productId = model.productId == 0 ? productId : model.productId;

                        result = await _context.saveUpdateValueViewModels.FromSql($"InvSetMaterialWiseSpecification {id},{model.productWiseSpecificationId},{model.productId},{model.skuName},{model.skuNumber},{model.imageUrl}").AsNoTracking().FirstOrDefaultAsync();
                        if (model.value != "")
                        {
                            await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSpecificationDetails {id},{model.specificationDetailsId},{result.isSuccess},{model.productCategorySpecificationId},{model.value}").AsNoTracking().FirstOrDefaultAsync();
                        }

                    }
                    else
                    {
                        if (model.value != "")
                        {
                            await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSpecificationDetails {id},{model.specificationDetailsId},{result.isSuccess},{model.productCategorySpecificationId},{model.value}").AsNoTracking().FirstOrDefaultAsync();
                        }
                    }
                    NewSKU = model.skuNumber;
                }
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> SaveProductWiseSize(int productId, string id, List<ProductWiseSizeViewModel> productWiseColor)
        {
            try
            {
                await _context.saveUpdateViewModels.FromSql($"InvDeleteProductWiseSize {id},{productId}").AsNoTracking().FirstOrDefaultAsync();
                var result = new SaveUpdateValueViewModel();

                foreach (ProductWiseSizeViewModel productwiseListViewModel in productWiseColor)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseSize {id},{productId},{productwiseListViewModel.sizeId},{productwiseListViewModel.Active},{productwiseListViewModel.isDefault}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductPricing(int productId, string id, List<ProductWisePricingViewModel> productPricing)
        {
            try
            {
                await _context.saveUpdateViewModels.FromSql($"InvDeleteProductPricing {id},{productId}").AsNoTracking().FirstOrDefaultAsync();
                var result = new SaveUpdateValueViewModel();

                foreach (ProductWisePricingViewModel productpricingListViewModel in productPricing)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductPricing {id},{productId},{productpricingListViewModel.colorId},{productpricingListViewModel.sizeId},{productpricingListViewModel.effectiveDate},{productpricingListViewModel.price},{productpricingListViewModel.barCode}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SaveProductDiscount(int productId, string id, ProductWiseDiscountViewModel productDiscount)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductDiscount {id},{productDiscount.discountId},{productDiscount.discountTypeId},{productId},{productDiscount.fromDate},{productDiscount.toDate},{productDiscount.discountAmountOrPercentage},{productDiscount.isAmount}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductJsonById(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetInvProductWiseSpecificationById(int productWiseSpecificationId, int productCategoryId, string skuNumber, string partslink, string interchange)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetInvProductWiseSpecificationJson {productWiseSpecificationId},{productCategoryId},{skuNumber},{partslink},{interchange}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaterialJsonById(int productId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetMaterialJson {productId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetLastPurchaseOrderDetailsBySpecId(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetLastPurchaseOrderDetailsBySpecId {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getTypeWiseProducts(int? userId, int productId, int productTypeId, string flag)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetTypeWiseProducts {productId},{productTypeId},{userId},{flag}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<JsonViewModel> getFinishedProducts()
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetFinishedProductJson").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        
        public async Task<JsonViewModel> getProductWiseBarCodeJsonById(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductWiseBarCodeJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getProductWiseBarCodeInUpdateJsonById(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductWiseBarCodeInUpdateJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getDiscountList(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetDiscountListJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductWiseSpecificationInUpdateJsonById(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSpecificationUpdateModeJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductWiseSupplierInUpdate(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSupplierUpdateModeJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductImage(string filePath)
        {
            //string[] array = filePath.Split(".");
            //string fileType = array[2];
            //byte[] b = System.IO.File.ReadAllBytes(filePath);
            //string img = $"data:image/{fileType};base64,{Convert.ToBase64String(b)}";

            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductImageJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        //public async Task<string> getProductImage(string filePath)
        //{
        //    byte[] b = System.IO.File.ReadAllBytes(filePath);
        //    var result =  ($"data:image/png;base64,{ Convert.ToBase64String(b)}");//.AsNoTracking().FirstOrDefaultAsync();

        //    var res = await _context.jsonViewModels.
        //    return await result;
        //}
        public async Task<JsonViewModel> getProductSupplierIdWise(int supplierId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetSupplierIdWiseJson {supplierId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getBrandByProductCategory(string Id,int productCategory)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetBrand {Id},{productCategory}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductById(string Id, int ProductId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProduct {Id},{ProductId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteProductDiscountById(string Id, int discountId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductDiscount {Id},{discountId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region product wise Details ---------

        public async Task<JsonViewModel> getProductWiseColor(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductColorUpdateModeJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductSpecificationInUpdate(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSpecificationInUpdateModeJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductWiseSize(int productId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSizeUpdateModeJson {productId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> getProductSpecification(int productCategoryId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductCategorySpecificationJson {productCategoryId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ProductViewModel>> getProduct()
        {
            try
            {
                var result = await _context.productViewModels.FromSql($"InvSpGetProduct {0}").AsNoTracking().ToListAsync();
                return result;

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<ProductViewModel>> getProduct(int productId)
        {
            var result = await _context.productViewModels.FromSql($"InvSpGetProduct {productId}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductWiseSpecificationViewModel>> getProductSKUNumber()
        {
            try
            {

                var result = await _context.productWiseSpecificationViewModels.FromSql($"InvSpGetProductWiseSpecification {0}").AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region  Product Set

        public async Task<int> SaveProductSetMaster(string userId, ProductSetMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductSetMaster {userId},{model.productSetMasterId},{model.companyId},{model.sbuId},{model.master_ProductWiseSpecificationId},{model.ProductSetName},{model.isActive},{model.isDelete}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        public async Task<int> SaveProductSetDetails(string userId, int productSetMasterId, List<ProductSetDetailsViewModel> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetProductSetDetails {userId},{model.productSetDetailsId},{productSetMasterId},{model.accessories_ProductWiseSpecificationId}, {model.qty},{model.isActive},{model.isDelete},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteProductSetMasterByMasterId(string userId, int productSetMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductSetMaster {userId},{productSetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteProductSetDetailsById(string userId, int productSetDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductSetDetails {userId},{productSetDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductSetMasterById(int productSetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSetMasterListJSON {productSetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductSetDetailsById(int productSetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSetDetailsListJSON {productSetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
         public async Task<JsonViewModel> GetProductSetReportById(int productSetMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSetReportJSON {productSetMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion  Product Set

        #region  Product Wise Color

        public async Task<int> SaveProductWiseColor(string userId, ProductWiseColorViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseColor {userId},{model.productWiseColorId},{model.productWiseSpecificationId},{model.colorCode},{model.minRange},{model.maxRange},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
                //await _context.saveUpdateViewModels.FromSql($"InvDeleteProductWiseColor {id},{productId}").AsNoTracking().FirstOrDefaultAsync();
                //var result = new SaveUpdateValueViewModel();
                //foreach (ProductWiseColorViewModel productwiseListViewModel in productWiseColor)
                //{
                //    result = await _context.saveUpdateValueViewModels.FromSql($"InvSetProductWiseColor {id},{productId},{productwiseListViewModel.colorId},{productwiseListViewModel.Active},{productwiseListViewModel.isDefault}").AsNoTracking().FirstOrDefaultAsync();
                //}
                //return result.isSuccess;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetProductWiseColorById(int productWiseColorId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductWiseColor {productWiseColorId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteProductWiseColorById(string userId, int productWiseColorId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvDeleteProductWiseColor {userId},{productWiseColorId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> VarifyPromoProductUploadData(string userId, string skuNumber, string productCode)
        {
            
            try
            {
                if (skuNumber == "undefined")
                {
                    skuNumber = null;
                }
                    var result = await _context.jsonViewModels.FromSql($"InvSpGetProductVarified {userId},{skuNumber}").AsNoTracking().FirstOrDefaultAsync();
                    return result;
                

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UploadPromoProduct(string userId, List<PromoProductUploadViewModel> products)
        {

            try
            {
                int counter = 0;
                foreach(PromoProductUploadViewModel product in products)
                {
                    string skuNumber = product.skuNumber.Replace("'", "`");
                    string skuName = product.skuName.Replace("'", "`"); 
                    //string packSize = product.packSize.Replace("'", "`");
                    string productCategory = product.productCategory.Replace("'", "`");
                    string sql = $"InvSetPromoProduct {userId}, {skuNumber}, {skuName}, {product.brand},{productCategory},{product.brand}";

                    var result = await _context.saveUpdateValueViewModels.FromSql($"InvSetPromoProduct {userId}, {skuNumber}, {skuName}, {product.brand},{productCategory},{product.brand}").AsNoTracking().FirstOrDefaultAsync();
                    if (result.isSuccess == 1)
                        counter += 1;
                }
                return counter == products.Count ? 1 : 0;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetAllPromoUploadedProducts(int productId)
        {

            try
            {
               
                var result = await _context.jsonViewModels.FromSql($"InvSpGetPromoUploadedProduct {productId}").AsNoTracking().FirstOrDefaultAsync();
                return result;


            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteProductWiseSpectById(string userId, int spectId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvDeleteInvProductWiseSpecification {userId},{spectId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region  Product Spec Info

        public async Task<bool> SaveProductSpecInfo(string id, ProductSpecInfoViewModel productSpecInfoViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpSetProductSpecInfo {id},{productSpecInfoViewModel.productSpecInfoId},{productSpecInfoViewModel.productWiseSpecificationId},{productSpecInfoViewModel.specificationDetails},{productSpecInfoViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetProductSpecInfoById(int productSpecInfoId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetProductSpecInfoJson {productSpecInfoId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteProductSpecInfoById(string id, int productSpecInfoId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteProductSpecInfo {id},{productSpecInfoId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion
    }
}
