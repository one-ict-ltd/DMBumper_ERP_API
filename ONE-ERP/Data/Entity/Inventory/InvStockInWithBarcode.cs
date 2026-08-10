using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvStockInWithBarcode: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int barcodeId { get; set; }
        [MaxLength(15)]
        public string barcodeNo { get; set; }        
        public string remarks { get; set; }
        public DateTime stockInDate { get; set; }
        public int storeId { get; set; }
        public int productWiseSpecificationId { get; set; }
        public decimal receiveQty { get; set; }
        public bool hasSerial { get; set; }
        public int partyId { get; set; }
        public decimal purchasePrice { get; set; }
    }
}
