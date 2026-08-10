namespace ONEERP.Areas.Inventory.Models
{
    public class ProductSizeViewModel
    {
        public int? sizeId { get; set; }        
        public decimal? size { get; set; }
        public int? uomId { get; set; }
        public string uomName { get; set; }        
        public bool? isActive { get; set; }
    }
}
