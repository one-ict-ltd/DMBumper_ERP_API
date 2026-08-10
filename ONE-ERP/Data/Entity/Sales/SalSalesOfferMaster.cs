using ONEERP.Data.Entity.Accounting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesOfferMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesOfferId { get; set; }
        [MaxLength(20)]
        public string salesOfferNo { get; set; }
        public DateTime? salesOfferDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? storeId { get; set; }
        [MaxLength(20)]
        public string mobileNo { get; set; }
        [MaxLength(20)]
        public string alternateMobileNo { get; set; }
        public string address { get; set; }
        public decimal? totalGross { get; set; }
        public decimal? totalVat { get; set; }
        public decimal? totalAit { get; set; }
        public decimal? shippingCost { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? grandTotal { get; set; }
        public int? approvalStatus { get; set; }
    }
}
