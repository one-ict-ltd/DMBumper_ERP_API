using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesReturnMasterViewModel
    {
        public int salesReturnMasterId { get; set; }
        public int? salesInvoiceId { get; set; }
        public int? partyId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? storeId { get; set; }
        public string salesReturnNo { get; set; }
        public DateTime? salesReturnDate { get; set; }
        public decimal? grossAmount { get; set; }
        public decimal? totalVatAmount { get; set; }
        public decimal? totalAitAmount { get; set; }
        public decimal? shippingCostAmount { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? netAmount { get; set; }
        public decimal? returnQty { get; set; }
        public bool? isClose { get; set; }
        public List<SalesReturnDetailsViewModel> lstDetailsViewModel { get; set; }
        public List<SalesGrossReturnInvoiceViewModel> lstInvoiceDetails { get; set; }
    }

    public class SalesGrossReturnInvoiceViewModel
    {
        public int? salesReturnMasterId { get; set; }
        public int? salSalesGrossReturnMasterId { get; set; }
        public int? salesInvoiceId { get; set; }
        public decimal? collectionAmount { get; set; }
        public bool? isSelect { get; set; }
    }
}
