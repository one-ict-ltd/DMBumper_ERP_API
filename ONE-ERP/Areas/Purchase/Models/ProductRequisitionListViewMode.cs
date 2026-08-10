using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurProductRequisitionListViewModel
    {
        public int? prodReqId { get; set; }
        public DateTime? prodReqDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public int? toWarehouseId { get; set; }
        public bool? isActive { get; set; }
        public List<ProductReqDetailsViewModel> lstReqDetailsViewModel { get; set; }
    }
}
