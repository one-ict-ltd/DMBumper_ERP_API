using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockReceiveViewModel
    {
        public int stockReceiveId { get; set; }
        public string stockReceiveNo { get; set; }
        public int? prodTrnfrId { get; set; }
        public DateTime stockReceiveDate { get; set; }
        public int? SbuId { get; set; }
        public string purpose { get; set; }
        public string receiveType { get; set; }
        public bool? isActive { get; set; }

        public List<StockReceiveDetailsViewModel> lstDetailsViewModel { get; set; }

    }
}
