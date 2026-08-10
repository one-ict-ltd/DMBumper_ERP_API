using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvStockInWithBarcodeDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int barcodeDetailsId { get; set; }
        public int? barcodeId { get; set; }
        public InvStockInWithBarcode barcode { get; set; }
        public string serialNo { get; set; }
        public bool? isSale { get; set; }
    }
}
