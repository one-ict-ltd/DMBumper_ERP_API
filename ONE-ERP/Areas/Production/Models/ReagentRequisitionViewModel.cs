using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class ReagentRequisitionViewModel
    {
        public int? reagentReqId { get; set; }
        public int? prodTrnfrId { get; set; }
        public DateTime? reagentReqDate { get; set; }
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
        public List<ReagentReqDetailsViewModel> lstReqDetailsViewModel { get; set; }

        //public int? productReqId { get; set; }
        //public DateTime? prodTrnDate { get; set; }
        //public string approvalStatus { get; set; }
    }
}
