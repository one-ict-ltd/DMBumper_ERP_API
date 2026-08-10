using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesInvoiceViewModel
    {
        public int? salesInvoiceId { get; set; }
        public string salesInvoiceNo { get; set; }
        public DateTime? salesInvoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? partyId { get; set; }
        public int? storeId { get; set; }
        public string mobileNo { get; set; }
        public string alternateMobileNo { get; set; }
        public string address { get; set; }
        public decimal? totalGross { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? shippingCost { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? grandTotal { get; set; }
        public string approvalStatus { get; set; } //0='Pending', 1='Approve', 2='Rejected/Cancelled'; if needed  3='Shipped', 4='Received', 5='OnHold', 6='Refund'
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public string refNo { get; set; }
        public int? transactionTypeId { get; set; }
        public string flyingCustomer { get; set; }
        public List<SalesInvoiceViewModel> lstMasterViewModel { get; set; }
        public List<SalesInvoiceDetailsViewModel> lstDetailsViewModel { get; set; }
        public List<SalesInvoiceTandCViewModel> tcLstDetailsViewModel { get; set; }
        public List<SalesCreditNoteViewModel> lstCreditNoteViewModel { get; set; }
    }
    public class SalesOrderViewModel
    {
        public int? salesOrderId { get; set; }
        public string salesOrderNo { get; set; }
        public DateTime? salesOrderDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? partyId { get; set; }
        public int? storeId { get; set; }
        public string mobileNo { get; set; }
        public string alternateMobileNo { get; set; }
        public string address { get; set; }
        public decimal? totalGross { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? shippingCost { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? grandTotal { get; set; }
        public string approvalStatus { get; set; } //0='Pending', 1='Approve', 2='Rejected/Cancelled', 3='Shipped', 4='Received', 5='OnHold', 6='Refund'
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public string refNo { get; set; }
        public int? transactionTypeId { get; set; }
        public string flyingCustomer { get; set; }
        public List<SalesOrderViewModel> lstMasterViewModel { get; set; }
        public List<SalesOrderDetailsViewModel> lstDetailsViewModel { get; set; }
        public List<SalesOrderTandCViewModel> tcLstDetailsViewModel { get; set; }
        //public List<SalesCreditNoteViewModel> lstCreditNoteViewModel { get; set; }
    }
    public class SalesInvPartyViewModel 
    {
        public int? partyId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public int? partyTypeId { get; set; }
        public string partyName { get; set; }
        public string partyMobile { get; set; }
        public string partyAddress { get; set; }
        public string territoryCode { get; set; }
    }

    public class SalesPickingViewModel 
    {
        public int? distributionMasterId { get; set; }
        public DateTime pickingDate { get; set; }
        public List<SalesPickingDetailViewModel> lstProductListViewModel { get; set; }
        public List<SalesPickingSummaryViewModel> lstMasterViewModel { get; set; }
    }

    public class SalesDispatchViewModel 
    {
        public int? distributionMasterId { get; set; }
        public int? employeeId { get; set; }
        public DateTime? startDate { get; set; }
        public List<SalesDispatchDetailsViewModel> lstMasterViewModel { get; set; }
    }


    public class DestructionNoteApprovalViewModel
    {
        public int? damageExpireProductReturnMasterId { get; set; }
        public bool? isSelect { get; set; }
        public int? approvalStatusValue { get; set; }
        public List<DestructionNoteApprovalViewModel> lstMasterViewModel { get; set; }
    }

    public class DamageExpireProductsReturnViewModel 
    {
        public int? damageExpireProductReturnMasterId { get; set; }
        public int? typeId { get; set; }
        public DateTime? startDate { get; set; }
        public string MarketOrDepo { get; set; }
        public List<DamageExpireProductsReturnDetailsViewModel> lstMasterViewModel { get; set; }
    }

    public class SalesPickingDetailViewModel 
    {
        public int? productWiseSpecificationId { get; set; }
        public decimal? invoiceQty { get; set; }
        public string skuNumber { get; set; }
    }

    public class SalesPickingSummaryViewModel 
    {
        public int? salesInvoiceId { get; set; }
        public string salesInvoiceNo { get; set; }
        public bool? isSelect { get; set; }
    }
    

    public class SalesDispatchDetailsViewModel
    {
        public int? pickingMasterId { get; set; }
        public bool? isSelect { get; set; }
        public int? salesInvoiceId { get; set; }
    }
    
    

    public class DamageExpireProductsReturnDetailsViewModel
    {
        public int miscellaneousItemId { get; set; }
        public bool isSelect { get; set; }
        public int miscellaneousItemDetailsId { get; set; }
        public decimal qty { get; set; }
        public int productSpecificationId { get; set; }
    }


    public class SalesCreditNoteViewModel
    {
        //				returnDate	amount	partyName	partyId	isSelect
        public int? productExpireReturnMasterId { get; set; }
        public int? productExpireReturnId { get; set; }
        public int? partyId { get; set; }
        public int? salesInvoiceId { get; set; }
        public bool? isSelect { get; set; }
    }


    //Sales invoice generate from sales Order 
    public class GenerateInvoiceViewModel
    {
        public int approvalType { get; set; }
        public List<ApprovedSalesOrderViewModel> lstApprovedOrderList { get; set; }
        //public List<SalesCreditNoteViewModel> lstCreditNoteViewModel { get; set; }
    }
    public class ApprovedSalesOrderViewModel
    {
        public int salesOrderId { get; set; }
        public bool isSelect { get; set; }
        public List<SalesCreditNoteViewModel> lstCreditNoteViewModel { get; set; }
    }
    public class DeleteSalesOrderViewModel
    {
        public int salesOrderId { get; set; }
    }
}
