using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalesCategoryWiseProductMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesCategoryWiseProductMasterId { get; set; }
        public int monthId { get; set; }
        public string year { get; set; }
        public int salCategorySalesId { get; set; }
        public SalCategorySales salCategorySales { get; set; }
    }
}
