using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesReturnDetailsViewModel
    {
        public int salesReturnDetailId { get; set; }
        public int? salesReturnMasterId { get; set; }
        public int? salesInvDetailsId { get; set; }
        public decimal? returnQty { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? price { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? totalPrice { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string batchNo { get; set; }

        public decimal? tp { get; set; }
        public decimal? vat { get; set; }
        public decimal? discount { get; set; }
    }
}
