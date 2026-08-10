namespace ONEERP.Areas.Inventory.Models
{
    public class ProductBrandViewModel
    {
        public int? brandId { get; set; }        
        public string brandName { get; set; }
        public string brandCode { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }
    }
}
