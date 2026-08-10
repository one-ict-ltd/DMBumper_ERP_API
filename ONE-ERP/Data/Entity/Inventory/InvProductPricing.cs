using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductPricing : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pricingId { get; set; }
        public int? productId { get; set; }
        public InvProduct product{get;set;}
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public DateTime? effectiveDate { get; set; }
        public decimal? avgPurchasePrice { get; set; }
        public decimal? price { get; set; }
        public decimal? unitVat { get; set; }
        public decimal? tradePrice { get; set; }
        public decimal? nationalFlatRate { get; set; }
        [MaxLength(250)]
        public string barCode { get; set; }
        [Column(TypeName = "image")]
        public byte[] barCodeImage { get; set; }
        public int? barcodeId { get; set; }
        public InvStockInWithBarcode invStockInWithBarcode { get; set; }
        public decimal? minimumSalePrice { get; set; }
    }
}
