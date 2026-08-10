using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProduct:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        [MaxLength(50)]
        public string productCode { get; set; }
        [MaxLength(250)]
        public string productName { get; set; }
        public decimal? width { get; set; }
        public decimal? height { get; set; }
        public decimal? weight { get; set; }
        [DefaultValue(0)]
        public bool? isQCRequired { get; set; }
        [MaxLength(50)]
        public string hsCODE { get; set; }
        public string genericName { get; set; }
        public string therapeuticClass { get; set; }
        public string description { get; set; }
        [DefaultValue(0)]
        public decimal? warrantyDuration { get; set; }
        public DateTime? expiryDate { get; set; }
        public int? notificationDay { get; set; }

        public int? productTypeId { get; set; }
        public InvProductType productType { get; set; }

        public int? productCategoryId { get; set; }
        public InvProductCategory productCategory { get; set; }

        public int? productSubCategoryId { get; set; }
        public InvProductSubCategory productSubCategory { get; set; }

        public int? modelId { get; set; }
        public InvProductModel productModel { get; set; }

        public int? brandId { get; set; }
        public InvProductBrand productBrand { get; set; }

        public int? uomId { get; set; }
        public InvProductUOM productUOM { get; set; }

        public int? originCountryId { get; set; }
        public CmnOriginCountry originCountry { get; set; }

        public int? gradeId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public bool? isPromoUploadedProduct { get; set; }

    }
}
