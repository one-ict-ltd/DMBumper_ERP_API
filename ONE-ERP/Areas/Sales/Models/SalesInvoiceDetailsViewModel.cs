using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesInvoiceDetailsViewModel
    {
        public int? salesInvDetailsId { get; set; }
        public int? salesInvoiceId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? invoiceQty { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public decimal? ait { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? Total { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public bool? hasNationalBonus { get; set; }
        public int? barcodeId { get; set; }
        public string serialNo { get; set; }
        public string batchNo { get; set; }
    }
    public class SalesOrderDetailsViewModel
    {
        public int? salesOrderDetailsId { get; set; }
        public int? salesOrderId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? orderQty { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public decimal? ait { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? Total { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public bool? hasNationalBonus { get; set; }
        public int? barcodeId { get; set; }
        public string serialNo { get; set; }
        public string batchNo { get; set; }
    }
}
