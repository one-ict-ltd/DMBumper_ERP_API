using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ProductRequisitionViewModel
    {
        public int? prodReqId { get; set; }
        public int? prodTrnfrId { get; set; }
        public DateTime? prodReqDate { get; set; }
        //public int? fromWarehouseId { get; set; }
        //public int? toWarehouseId { get; set; }
        public int? fromsbuId { get; set; }
        public int? tosbuId { get; set; }
        public bool? isActive { get; set; }
        public bool? isDelete { get; set; }
        public string purpose { get; set; }
        public string driverName { get; set; }
        public string vehicleNo { get; set; }
        public string transferType { get; set; }
        public bool? isUrgency { get; set; }
        public List<ProductReqDetailsViewModel> lstReqDetailsViewModel { get; set; }

        //public int? productReqId { get; set; }
        //public DateTime? prodTrnDate { get; set; }
        //public string approvalStatus { get; set; }

    }
}