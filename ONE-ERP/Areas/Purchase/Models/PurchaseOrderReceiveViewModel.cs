using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseOrderReceiveViewModel
    {
        public int? poReceiveId { get; set; }
        public int? purchaseOrderId { get; set; }
        public DateTime? purchaseOrderRecvDate { get; set; }
        public int? tosbuId { get; set; }
        public string approvalStatus { get; set; }
        public bool? isActive { get; set; }
        public List<PurchaseOrderReceiveDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
