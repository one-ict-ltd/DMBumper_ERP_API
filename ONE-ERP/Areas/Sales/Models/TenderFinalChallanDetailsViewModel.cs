using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class TenderFinalChallanDetailsViewModel
    {
        public int finalChallanDetailsId { get; set; }
        public int? challanDetailsId { get; set; }
        public int? challanMasterId { get; set; }
        public int quotationDetailsId { get; set; }
        public int? quotationMasterId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? challanQty { get; set; }
        public decimal? convertionQty { get; set; }
        public decimal? CtnQty { get; set; }
        public int? toUomId { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public decimal? unitVat { get; set; }
        public decimal? tradePrice { get; set; }
        public decimal? ait { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? Total { get; set; }
        public int? barcodeId { get; set; }
        public string serialNumber { get; set; }
        public string batchNo { get; set; }
        public string specification { get; set; }
        public string remarks { get; set; }
        public string deliveryStatus { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
    }
   
}
