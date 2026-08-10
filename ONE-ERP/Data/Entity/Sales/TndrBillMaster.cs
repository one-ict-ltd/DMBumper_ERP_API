using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class TndrBillMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int billMasterId { get; set; }
        public string billNo { get; set; }
        public DateTime? billDate { get; set; }
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
        public int? planId { get; set; }
        public string refNo { get; set; }
        public int? isClosed { get; set; }
        public string billStatus { get; set; }

    }
}
