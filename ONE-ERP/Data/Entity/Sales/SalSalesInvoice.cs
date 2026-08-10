using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesInvoice:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesInvoiceId { get; set; }
        [MaxLength(100)]
        public string salesInvoiceNo { get; set; }
        public DateTime? salesInvoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? storeId { get; set; }
        [MaxLength(50)]
        public string mobileNo { get; set; }
        [MaxLength(50)]
        public string alternateMobileNo { get; set; }
        public string address { get; set; }
        public decimal? totalGross { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? shippingCost { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? grandTotal { get; set; }
        public int? approvalStatus { get; set; } //0='Pending', 1='Approve', 2='Rejected/Cancelled', 3='Shipped', 4='Received', 5='OnHold', 6='Refund'
        public int? planId { get; set; }
        public int? chemistId { get; set; }
        [MaxLength(100)]
        public string refNo { get; set; }
        public string orderType { get; set; } //cash or chedit
        public int? transactionTypeId { get; set; }
        public CmnTransactionType transactionType { get; set; }
        public string territoryCode { get; set; }
        public string areaCode { get; set; }
        public string regionCode { get; set; }
        public string zoneCode { get; set; }
        public string territoryOfficer { get; set; }
        public string territoryOfficerName { get; set; }
        public string areaManager { get; set; }
        public string regionManager { get; set; }
        public string zoneManager { get; set; }
        public string depotCode { get; set; }
        public int? isClosed { get; set; }
        //public DateTime? gdnConfirmDate { get; set; }
        //public int? gdnConfirmBy { get; set; }

        public int? salesOrderId { get; set; }
        public SalSalesOrder salesOrder { get; set; }

        public int? challanMasterId { get; set; }
        public TndrChallanMaster challanMaster { get; set; }
        public int? billMasterId { get; set; }
        public TndrBillMaster billMaster { get; set; }
    }

    // 2024-Jun-13
    public class SalSalesOrder:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesOrderId { get; set; }
        [MaxLength(100)]
        public string salesOrderNo { get; set; }
        public DateTime? salesOrderDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? storeId { get; set; }
        [MaxLength(50)]
        public string mobileNo { get; set; }
        [MaxLength(50)]
        public string alternateMobileNo { get; set; }
        public string address { get; set; }
        public decimal? totalGross { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? shippingCost { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? grandTotal { get; set; }
        public int? approvalStatus { get; set; } //0='Pending', 1='Approve', 2='Rejected/Cancelled', 3='Shipped', 4='Received', 5='OnHold', 6='Refund'
        public int? planId { get; set; }
        public int? chemistId { get; set; }
        [MaxLength(100)]
        public string refNo { get; set; }
        public string orderType { get; set; } //cash or chedit
        public int? transactionTypeId { get; set; }
        public CmnTransactionType transactionType { get; set; }
        public string territoryCode { get; set; }
        public string areaCode { get; set; }
        public string regionCode { get; set; }
        public string zoneCode { get; set; }
        public string territoryOfficer { get; set; }
        public string territoryOfficerName { get; set; }
        public string areaManager { get; set; }
        public string regionManager { get; set; }
        public string zoneManager { get; set; }
        public string depotCode { get; set; }
        public int? isClosed { get; set; }
        //public DateTime? gdnConfirmDate { get; set; }
        //public int? gdnConfirmBy { get; set; }
    }
}
