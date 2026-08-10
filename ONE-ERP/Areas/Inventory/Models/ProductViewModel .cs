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
        public List<ProductWiseSpecificationViewModel> Specificationdetail { get; set; }


    }
}
