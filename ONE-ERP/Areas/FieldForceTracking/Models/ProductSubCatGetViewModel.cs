using System.Collections.Generic;

namespace ONEERP.Areas.FieldForceTracking.Models
{

    public class ProductSubCatGetViewModel
    {
        public int? productSubCategoryId { get; set; }        
        public string subCategoryName { get; set; }
        public List<ProductGetViewModel> Product { get; set; }

    }
}
