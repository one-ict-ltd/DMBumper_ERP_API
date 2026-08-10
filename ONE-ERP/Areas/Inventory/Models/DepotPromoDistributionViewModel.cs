using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class DepotPromoDistributionViewModel
    {
        public int promoDistributionMasterId { get; set; }
        public DateTime? promoDistributionDate { get; set; }
        public string promoDistributionNo { get; set; }
        public string Purpose { get; set; }
        public int? isReceived { get; set; }
        public List<DepotPromoDistributionDetailsViewModel> lstDetailsViewModel { get; set; }
        public int prodTrnfrId { get; set; }
        public int? fromSbuId { get; set; }
    }
}
