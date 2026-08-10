using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseRequisitionListViewMode
    {
        public int? purchaseReqId { get; set; }
        public int? productReqId { get; set; }
        public DateTime? purchaseReqDate { get; set; }
        public int? fromWarehouseId { get; set; }
        public int? toWarehouseId { get; set; }
        public string purpose { get; set; }
        public bool? isUrgency { get; set; }
        public bool? isActive { get; set; }
        public List<PurchaseReqDetailsViewModel> lstReqDetailsViewModel { get; set; }
    }
}
