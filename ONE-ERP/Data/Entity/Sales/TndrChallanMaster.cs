using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class TndrChallanMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int challanMasterId { get; set; }
        [MaxLength(100)]
        public string challanNo { get; set; }
        public DateTime? challanDate { get; set; }
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
        public string refNo { get; set; }
        public string orderType { get; set; } //cash or chedit
        public int? isClosed { get; set; }
        public bool? isFinal { get; set; }

    }
}
