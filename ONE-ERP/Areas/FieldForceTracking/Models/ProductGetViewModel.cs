namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ProductGetViewModel
    {
        public decimal? costPrice { get; set; }
        //public decimal? currentStock { get; set; }
        //public decimal? discountAmount { get; set; }
        public int? invoiceQty { get; set; }
        public decimal? price { get; set; }
        public decimal? unitVat { get; set; }
        public decimal? tradePrice { get; set; }
        public int? productId { get; set; }
        public string productName { get; set; }
        public int? productWiseSpecificationId { get; set; }
        //public int? saleUnit { get; set; }
        
        public int? salesInvDetailsId { get; set; }
        //public int? salesInvoiceId { get; set; }
        public decimal? totalPrice { get; set; }
        public int? uomId { get; set; }
        public string uomName { get; set; }

       
        
    }
}
