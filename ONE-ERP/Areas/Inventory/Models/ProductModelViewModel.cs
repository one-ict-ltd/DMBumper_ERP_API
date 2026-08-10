namespace ONEERP.Areas.Inventory.Models
{
    public class ProductModelViewModel
    {
        public int? modelId { get; set; }        
        public string modelName { get; set; }
        public string modelCode { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }
    }
}
