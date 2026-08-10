namespace ONEERP.Areas.Inventory.Models
{
    public class ProductTypeViewModel
    {
        public int? productTypeId { get; set; }        
        public string productTypeName { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
    }
}
