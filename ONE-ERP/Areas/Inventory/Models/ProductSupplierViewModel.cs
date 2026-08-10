namespace ONEERP.Areas.Inventory.Models
{
    public class ProductSupplierViewModel
    {
        public int? productsupplierId { get; set; } 
        public string skuName { get; set; }
        public string skuNumber { get; set; }
        public int? supplierId { get; set; }
        public int? productId { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }    
    }
}
