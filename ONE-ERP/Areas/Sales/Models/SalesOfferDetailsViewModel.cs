using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesOfferDetailsViewModel
    {
        public int? salesOfferDetailsId { get; set; }
        public int? salesOfferId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? salesOfferQty { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public decimal? ait { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? Total { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
    }
}
