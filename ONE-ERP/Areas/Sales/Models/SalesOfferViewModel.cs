using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesOfferViewModel
    {
        public int? salesOfferId { get; set; }
        public string salesOfferNo { get; set; }
        public DateTime? salesOfferDate { get; set; }
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
        public string approvalStatus { get; set; }
        public bool? isActive { get; set; }
        public List<SalesOfferDetailsViewModel> lstDetailsViewModel { get; set; }
    }
}
