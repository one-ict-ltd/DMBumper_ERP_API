using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class TenderChallanViewModel
    {
        public int challanMasterId { get; set; }
        public string challanNo { get; set; }
        public DateTime? challanDate { get; set; }
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
        public string refNo { get; set; }
        public string orderType { get; set; } //cash or chedit
        public int? isClosed { get; set; }
        public bool? isFinal { get; set; }
        public List<TenderChallanViewModel> lstMasterViewModel { get; set; }
        public List<TenderChallanDetailsViewModel> lstDetailsViewModel { get; set; }
        public List<TenderFinalChallanDetailsViewModel> finalChallanDetailsViewModel { get; set; }
    }
    
}
