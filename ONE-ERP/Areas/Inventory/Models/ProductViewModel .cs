using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductViewModel
    { 
        public int productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public decimal? width { get; set; }
        public decimal? height { get; set; }
        public decimal? weight { get; set; }
        public bool? isQCRequired { get; set; }
        public string hsCODE { get; set; }
        public string description { get; set; }
        public decimal? warrantyDuration { get; set; }
        public int? notificationDay { get; set; }
        public int? productTypeId { get; set; }
        public int?  productCategoryId { get; set; }
        public int? productSubCategoryId { get; set; }
        public int? modelId { get; set; }
        public int? brandId { get; set; }
        public int? uomId { get; set; }
        public int? originCountryId { get; set; }
        public int? gradeId { get; set; }
        public int? companyId { get; set; }
        public bool? isActive { get; set; }
        public DateTime? expiryDate { get; set; }

        //New added for BMBamper
        public int productWiseSpecificationId { get; set; }
        public string skuName { get; set; }
        public string skuNumber { get; set; }
        public string imageUrl { get; set; }
        public string partslink { get; set; }
        public string location { get; set; }
        public decimal? qtyonHand { get; set; }
        public string uom { get; set; }
        public decimal? listPrice { get; set; }
        public decimal? costPrice { get; set; }
        public decimal? salesPrice { get; set; }
        public int? fromYear { get; set; }
        public int? toYear { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string oem { get; set; }
        public string interchange { get; set; }
        public string patent { get; set; }
        public string side { get; set; }
        public string position { get; set; }
        public string material { get; set; }
        public string colorOrFinish { get; set; }
        public string certification { get; set; }
        public string status { get; set; }
        public string barcodeOrQR { get; set; }
        public decimal? productWeight { get; set; }
        public string productWeight_UOM { get; set; }
        public string productWidth { get; set; }
        public string productHeight { get; set; }
        public string productLength { get; set; }
        public string productSizeUOM { get; set; }
        public string productActive { get; set; }
        public string productTaxable { get; set; }
        public bool? isWebsiteActive { get; set; }
        public bool? isReturnable { get; set; }
        public decimal? warrantyDays { get; set; }
        public DateTime? lastReceivedDate { get; set; }
        public DateTime? lastSoldDate { get; set; }
        public string primaryVendor { get; set; }
        public string vendorType { get; set; }
        public string submodelOrTrim { get; set; }
        public string bodyStyle { get; set; }
        public string engineSize { get; set; }
        public string warehouse { get; set; }
        public string zone { get; set; }
        public string aisle { get; set; }
        public string rack { get; set; }
        public string shelf { get; set; }
        public string bin { get; set; }
        public string pickLocationOrZone { get; set; }
        public string bulkLocationOrZone { get; set; }
        public decimal? qtyReserved { get; set; }
        public decimal? qtyDamagedHold { get; set; }
        public decimal? qtyReceivingHold { get; set; }
        public decimal? qtyReturnIntake { get; set; }
        public decimal? qtyVendorReturn { get; set; }
        public decimal? qtyScrap { get; set; }
        public decimal? previousCountdays { get; set; }
        public DateTime? spotCountDate { get; set; }
        public decimal? currentCountDays { get; set; }
        public decimal? cycleCountFrequency { get; set; }
        public string abc_Class { get; set; }
        public decimal? leadTimeDays { get; set; }
        public decimal? safetyStock { get; set; }
        public decimal? minStock { get; set; }
        public decimal? maxStock { get; set; }
        public decimal? suggestedReorderQty { get; set; }
        public string vendorName { get; set; }
        public string defaultVendor { get; set; }
        public string vendorPartNumber { get; set; }
        public decimal? cost { get; set; }
        public string vendorUOM { get; set; }
        public string assetAccount { get; set; }
        public string cogsAccount { get; set; }
        public string adjustmentAccount { get; set; }
        public string scrapAccount { get; set; }
        public string varianceAccount { get; set; }
        public string incomeAccount { get; set; }
        public string upc { get; set; }
        public string partTypeID { get; set; }
        public string batchNumber { get; set; }
        public string notes { get; set; }
        public int? makeId { get; set; }
        public int? makeModelId { get; set; }

        public List<ProductWiseSpecificationViewModel> Specificationdetail { get; set; }


    }
}
