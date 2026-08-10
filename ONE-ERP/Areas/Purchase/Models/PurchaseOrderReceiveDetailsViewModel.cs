using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseOrderReceiveDetailsViewModel
    {
        public int? poReceiveDetailsId { get; set; }
        public int? poReceiveId { get; set; }
        public int? purchaseOrderDetailsId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? receiveQty { get; set; }
        public decimal? price { get; set; }
        public bool? isActive { get; set; }
    }
}
