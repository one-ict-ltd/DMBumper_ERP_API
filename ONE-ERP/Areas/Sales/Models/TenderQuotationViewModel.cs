using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class TenderQuotationViewModel
    {
        public int quotationMasterId { get; set; }
        public string quotationNo { get; set; }
        public DateTime? quotationDate { get; set; }
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
        public int? approvalStatus { get; set; } //0='Pending', 1='Approve', 2='Rejected/Cancelled', 3='Shipped', 4='Received', 5='OnHold', 6='Refund'
        public int? planId { get; set; }
        public int? chemistId { get; set; }
        public string refNo { get; set; }
        public string orderType { get; set; } //cash or chedit
        public int? transactionTypeId { get; set; }
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
        public List<TenderQuotationViewModel> lstMasterViewModel { get; set; }
        public List<TenderQuotationDetailsViewModel> lstDetailsViewModel { get; set; }
    }
    
}
