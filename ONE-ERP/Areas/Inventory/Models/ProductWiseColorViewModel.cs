namespace ONEERP.Areas.Inventory.Models
{
    public class ProductWiseColorViewModel
    {
        //public bool? Active { get; set; }
        //public int? productWiseColorId { get; set; }    
        //public int? colorId { get; set; }
        //public string colorName { get; set; }
        //public string colorCode { get; set; }
        //public int? productId { get; set; }
        //public bool? isActive { get; set; }
        //public bool? isDefault { get; set; }

        public int productWiseColorId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string colorCode { get; set; }
        public decimal? minRange { get; set; }
        public decimal? maxRange { get; set; }
        public bool? isActive { get; set; }
    }
}
