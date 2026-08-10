using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class DepotPromoReceiveViewModel
    {
        public int depotPromoReceiveMasterId { get; set; }
        public DateTime? packetDistributionDate { get; set; }
        public string promoReceivedNo { get; set; }
        public int? fromSbuId { get; set; }
        public string purpose { get; set; }
        public int packetDistributionId { get; set; }
        public List<DepotPromoReceiveDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
