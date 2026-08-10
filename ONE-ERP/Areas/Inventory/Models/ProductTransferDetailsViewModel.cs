using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductTransferDetailsViewModel
    {

        public int? productTrnfrDetailsId { get; set; }
        public int? prodTrnfrId { get; set; }
        public int? productReqDetailsId { get; set; }
        public int? fromStoreId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? transferQty { get; set; }
        public decimal? price { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public string batchNo { get; set; }

    }
}
