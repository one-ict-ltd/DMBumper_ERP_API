using System;

namespace ONEERP.Areas.Inventory.Models
{
    public class ProductPricingViewModel
    {
        public int? pricingId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public DateTime? effectiveDate { get; set; }
        public decimal? avgPurchasePrice { get; set; }
        public decimal? price { get; set; }
        public decimal? tradePrice { get; set; }
        public decimal? unitVat { get; set; }
        public string barcodeNo { get; set; }
        public int? barcodeId { get; set; }
        public decimal? minimumSalePrice { get; set; }
    }

    public class CashSetUpViewModel
    {
        public int? employeeId { get; set; }
        public string employeeNo { get; set; }
        public string fullName { get; set; }
        public string currentDesignation { get; set; }
        public string currentDepartment { get; set; }
        public string salaryLocation { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? walletAmount { get; set; }
        public decimal? defaultAmount { get; set; }
    }
}
