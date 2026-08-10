using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseReqDetailsViewModel
    {

        public int? purchaseReqDetailsId { get; set; }
        public int? purchaseReqId { get; set; }
        public int? productReqDetailsId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public bool? isActive { get; set; }
        public decimal? Total { get; set; }
        public bool? isSelect { get; set; }
        public string comments { get; set; }
        public string prodSpecification { get; set; }
        public int? purchaseOrderDetailId { get; set; }
        public decimal? receivedQty { get; set; }
        public decimal? currentStockQty { get; set; }
        public decimal? vatAmount { get; set; }

        public int? approvalLogId { get; set; }
        public int? revisionId { get; set; }


    }
}
