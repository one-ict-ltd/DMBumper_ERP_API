using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockReceiveDetailsViewModel
    {
        public int stockReceiveDetailsId { get; set; }
        public int? stockReceiveId { get; set; }
        public int? productTrnfrDetailsId { get; set; }
        public int? storeId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string stockReceiveQty { get; set; }
        public string batchNo { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
    }
}
