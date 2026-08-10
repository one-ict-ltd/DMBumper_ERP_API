using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class StockInWithBarcode
    {
        public int barcodeId { get; set; }
        public string barcodeNo { get; set; }
        public string remarks { get; set; }
        public DateTime stockInDate { get; set; }
        public int storeId { get; set; }
        public int productWiseSpecificationId { get; set; }
        public double receiveQty { get; set; }
        public bool isActive { get; set; }
        public bool isSelect { get; set; }
        public bool hasSerial { get; set; }

        public int partyId { get; set; }
        public decimal purchasePrice { get; set; }

        public List<StockInWithBarcodeDetails> lstDetailsViewModel { get; set; }
    }
}
