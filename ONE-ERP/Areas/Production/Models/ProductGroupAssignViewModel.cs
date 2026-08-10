using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class ProductGroupAssignViewModel
    {
        public int? prdGroupAssignId { get; set; }
        public int? phGroupMasterId { get; set; }
        public List<ProductGroupWiseItems> lstDetailsViewModel { get; set; }
    }

    public class ProductGroupWiseItems
    {
        public int? productWiseSpecificationId { get; set; }
        public bool isSelect { get; set; }
    }
}
