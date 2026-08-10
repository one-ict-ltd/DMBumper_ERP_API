using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class TenderBillViewModel
    {
        public int billMasterId { get; set; }
        public string billNo { get; set; }
        public DateTime? billDate { get; set; }
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
        public string billStatus { get; set; }
        public int? planId { get; set; }
        public string refNo { get; set; }
        public int? isClosed { get; set; }
        public List<TenderBillViewModel> lstMasterViewModel { get; set; }
        public List<TenderBillDetailsViewModel> lstDetailsViewModel { get; set; }
    }

}
