namespace ONEERP.Areas.Inventory.Models
{
    public class ProductDiscountTypeViewModel
    {
        public int? discountTypeId { get; set; }        
        public string discountTypeName { get; set; }
        public string discountTypeCode { get; set; }
        public string aliasName { get; set; }
        public bool? isActive { get; set; }
    }
}
