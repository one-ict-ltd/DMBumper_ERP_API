using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalProductForDiscountViewModel
    {
        public int? productWiseSpecificationId { get; set; }
        public decimal? price { get; set; }
        public string discountType { get; set; }
        public decimal? percentAmount { get; set; }
        public bool? isActive { get; set; }
    }
}
