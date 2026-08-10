namespace ONEERP.Areas.Inventory.Models
{
    public class ProductWiseSpecificationDetailsViewModel
    {
        public int? specificationDetailsId { get; set; }        
        public int? productWiseSpecificationId { get; set; }
        public int? productCategorySpecificationId { get; set; }
        public string value { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }   


    }
}
