namespace ONEERP.Areas.Inventory.Models
{
    public class ProductSpecInfoViewModel
    {
        public int productSpecInfoId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string specificationDetails { get; set; }
        public bool? isActive { get; set; }
    }
}
