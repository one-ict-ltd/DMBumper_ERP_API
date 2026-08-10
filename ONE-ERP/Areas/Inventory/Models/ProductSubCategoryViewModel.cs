namespace ONEERP.Areas.Inventory.Models
{
    public class ProductSubCategoryViewModel
    {
        public int? productSubCategoryId { get; set; }
        public int? productCategoryId { get; set; }
        public string subCategoryName { get; set; }
        public string aliasName { get; set; }
        public int? parentId { get; set; }
        public bool? isActive { get; set; }
    }
}
