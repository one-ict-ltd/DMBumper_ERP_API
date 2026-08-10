using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesDistributionMasterViewModel
    {
        public int? distributionMasterId { get; set; }
        public string distributionNumber { get; set; }
        public DateTime? distributionDate { get; set; }
        public string deliveryManName { get; set; }
        public string deliveryManMobile { get; set; }
        public string driverName { get; set; }
        public string driverMobile { get; set; }
        public string vehicleNo { get; set; }
        public string deliveryAddress { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public string approvalStatus { get; set; }
        public List<SalesDistributionDetailsViewModel> lstDetailsViewModel { get; set; }
        public List<SalesDistributionMasterViewModel> lstMasterViewModel { get; set; }
    }

    public class SalesDistributionDetailsViewModel
    {
        public int? distributionDetailId { get; set; }
        public int? distributionMasterId { get; set; }
        public int? salesInvoiceId { get; set; }
        public int? salesInvDetailsId { get; set; }
        public decimal? invoiceQty { get; set; }
        public decimal? distributionQty { get; set; }
        public bool? isSelect { get; set; }
    }
}
