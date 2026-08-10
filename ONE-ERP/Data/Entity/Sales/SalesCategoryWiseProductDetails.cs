using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalesCategoryWiseProductDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesCategoryWiseProductDetails { get; set; }
        public int productId { get; set; }
        public int? isChecked { get; set; }
        public int salesCategoryWiseProductMasterId { get; set; }
        public SalesCategoryWiseProductMaster salesCategoryWiseProductMaster { get; set; }
    }
}
