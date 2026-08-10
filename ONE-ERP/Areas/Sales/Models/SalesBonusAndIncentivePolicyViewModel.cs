using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Sales.Models
{
    public class SalGeneralCustomerBonusPolicyViewModel
    {
        public int? generalPolicyId { get; set; }
        public int? fromDays { get; set; }
        public int? toDays { get; set; }
        public int? maxDays { get; set; }
        public decimal? percentValue { get; set; }
        public int? companyId { get; set; }
        public bool isActive { get; set; }
        //public List<SalGeneralCustomerBonusPolicyViewModel> lstGeneralCustomerBonusPolicyViewModel { get; set; }
    }

    public class SalDiscountRateViewModel
    {
        public int DiscountRateId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? partyId { get; set; }
        public decimal? price { get; set; }
        public string discountType { get; set; }
        public decimal? percentAmount { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? amount { get; set; } // final amount
        public DateTime? fromDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool isActive { get; set; }
        public List<SalProductForDiscountViewModel> selectedProductList { get; set; }

    }

    public class SalDiscountItemViewModel
    {
        public int DiscountItemId { get; set; }
        public int? bonusforSpecificationId { get; set; }
        public decimal? forQuantity { get; set; }
        public int? partyId { get; set; }
        public int? bonusSpecificationId { get; set; }
        public decimal? quantity { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool isActive { get; set; }
        //public List<SalDiscountItemViewModel> listViewModel { get; set; }
        
    }


    public class SalMangoCustomerBonusPolicyViewModel
    {
        public int? mangoPolicyId { get; set; }
        public int? fromMonth { get; set; }
        public int? toMonth { get; set; }
        public DateTime? paymentDate { get; set; }
        public decimal? percentValue { get; set; }
        public bool isActive { get; set; }
        //public List<SalMangoCustomerBonusPolicyViewModel> lstMangoCustomerBonusPolicyViewModel { get; set; }
    }
    public class SalProductSpecWiseIncentivePolicyViewModel
    {
        public int? incentivePolicyId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? collUpToDays { get; set; }
        public decimal minOrderQty { get; set; }
        public string uom { get; set; }
        public string incentiveType { get; set; }
        public decimal? incentiveValue { get; set; }
        public DateTime? effectiveDate { get; set; }
        public DateTime? toDate { get; set; }
        public bool isActive { get; set; }
        //public List<SalProductSpecWiseIncentivePolicyViewModel> lstProductSpecWiseIncentivePolicyViewModel { get; set; }
    }
    public class SalDiscountPolicyUpdateViewModel
    {
        public string tableName { get; set; }
        public int? tableId { get; set; }
        public bool isSelect { get; set; }
        //public int? partyId { get; set; }
        //public decimal? price { get; set; }
        //public string discountType { get; set; }
        //public decimal? percentAmount { get; set; }
        //public decimal? discountAmount { get; set; }
        //public decimal? amount { get; set; } // final amount
        //public DateTime? fromDate { get; set; }
        //public DateTime? endDate { get; set; }
        //public bool isActive { get; set; }
        //public List<SalProductForDiscountViewModel> selectedProductList { get; set; }

    }
}
