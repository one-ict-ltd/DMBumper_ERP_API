using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesInvoiceDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesInvDetailsId { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salSalesInvoice { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? invoiceQty { get; set; }
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
        public bool? hasNationalBonus { get; set; }

        public int? salesOrderDetailsId { get; set; }
        public SalSalesOrderDetails salesOrderDetails { get; set; }

        public int? finalChallanDetailsId { get; set; }
        public TndrFinalChallanDetails finalChallanDetails { get; set; }
        public int? billDetailsId { get; set; }
        public TndrBillDetails billDetails { get; set; }
    }
    public class SalSalesOrderDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesOrderDetailsId { get; set; }
        public int? salesOrderId { get; set; }
        public SalSalesOrder salesOrder { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? orderQty { get; set; }
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
        public bool? hasNationalBonus { get; set; }
    }
}
