using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductTransferViewModel
    {
        public int? prodTrnfrId { get; set; }
        public int? productReqId { get; set; }
        public DateTime? prodTrnDate { get; set; }
        public int? fromsbuId { get; set; }
        public int? tosbuId { get; set; }
        public string approvalStatus { get; set; }
        public string purpose { get; set; }
        public bool? isUrgency { get; set; }
        public bool? isActive { get; set; }
        public string transferType { get; set; }
        public string driverName { get; set; }
        public string vehicleNo { get; set; }
        public List<ProductTransferDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
