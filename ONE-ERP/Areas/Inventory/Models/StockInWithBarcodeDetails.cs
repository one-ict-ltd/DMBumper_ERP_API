using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockInWithBarcodeDetails
    {
        public int? barcodeDetailsId { get; set; }
        public int? barcodeId { get; set; }
        public string serialNo { get; set; }
        public bool? isActive { get; set; }
        public bool? isSale { get; set; }
    }
}
