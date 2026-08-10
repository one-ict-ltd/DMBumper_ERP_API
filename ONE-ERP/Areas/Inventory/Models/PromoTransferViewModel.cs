using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class PromoTransferViewModel
    {
        public int promoTrnfId { get; set; }
        public DateTime? packetDistributionDate { get; set; }
        public string packetDistributionNo { get; set; }
        public int? fromSbuId { get; set; }
        public int? toSbuId { get; set; }
        public int? fromStoreId { get; set; }
        public string Purpose { get; set; }
        public List<PromoTransferDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
