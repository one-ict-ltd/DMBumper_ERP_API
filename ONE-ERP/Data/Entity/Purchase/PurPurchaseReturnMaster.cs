using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurPurchaseReturnMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int purchaseReturnMasterId { get; set; }
        public int? purchaseOrderId { get; set; }
        public PurPurchaseOrder purchaseOrder { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? storeId { get; set; }
        public CmnStore store { get; set; }

        [MaxLength(100)]
        public string purchaseReturnNo { get; set; }
        public DateTime? purchaseReturnDate { get; set; }
        public decimal? grossAmount { get; set; }
        public decimal? totalVatAmount { get; set; }
        public decimal? totalAitAmount { get; set; }
        public decimal? freightChargeAmount { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? netAmount { get; set; }
        public string comments { get; set; }
        public bool? isClose { get; set; }
    }
}
