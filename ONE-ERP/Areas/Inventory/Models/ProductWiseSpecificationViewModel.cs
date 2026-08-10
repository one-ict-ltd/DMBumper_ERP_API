using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductWiseSpecificationViewModel
    {
        public int productWiseSpecificationId { get; set; }
        public int? productCategorySpecificationId { get; set; }
        public int? specificationDetailsId { get; set; }
        public int? productId { get; set; }
        public string skuName { get; set; }
        public string skuNumber { get; set; }
        public string value { get; set; }
        public bool? isActive { get; set; }
        [NotMapped]
        public string imageFile { get; set; }
        public bool isUpdate { get; set; }
        public string imageUrl { get; set; }


    }
}
