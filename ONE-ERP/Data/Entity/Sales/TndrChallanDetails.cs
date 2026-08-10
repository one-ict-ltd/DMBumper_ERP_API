using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class TndrChallanDetails: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int challanDetailsId { get; set; }
        public int? challanMasterId { get; set; }
        public TndrChallanMaster challanMaster { get; set; }
        public int quotationDetailsId { get; set; }
        public int? quotationMasterId { get; set; }
        public TndrQuotationMaster quotationMaster { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? challanQty { get; set; }
        public decimal? convertionQty { get; set; }
        public decimal? CtnQty { get; set; }
        public int? toUomId { get; set; }
        public decimal? price { get; set; }
        public decimal? vat { get; set; }
        public decimal? unitVat { get; set; }
        public decimal? tradePrice { get; set; }
        public decimal? ait { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? Total { get; set; }
        public int? barcodeId { get; set; }
        public InvStockInWithBarcode invStockInWithBarcode { get; set; }
        public string serialNumber { get; set; }
        public string batchNo { get; set; }
        public string specification { get; set; }
        public string remarks { get; set; } 
    }
    
}
