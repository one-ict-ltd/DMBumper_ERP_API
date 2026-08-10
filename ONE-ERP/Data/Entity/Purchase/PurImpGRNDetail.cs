using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpGRNDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int grnDetailsId { get; set; }

        public int? grnMasterId { get; set; }
        public PurImpGRNMaster grnMaster { get; set; }

        public int? PurImpPreLCInfoDetailId { get; set; }
        public PurImpPreLCInfoDetail PurImpPreLCInfoDetail { get; set; }

        public decimal? receivedQty { get; set; } // PO actualRcvQty
        public decimal? price { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? actualAmount { get; set; }

        public decimal? actualRcvQty { get; set; } //  actualRcvQty
        public int? toUOMId { get; set; }
        public InvProductUOM toUOM { get; set; }
        public decimal? potency { get; set; }
        public decimal? approvedQty { get; set; }
        public decimal? rejectedQty { get; set; }

        public DateTime? mfgDate { get; set; }
        public DateTime? expiryDate { get; set; }
        public int noOfBag { get; set; }
        public string batchNo { get; set; }
        public string manufactureOrigin { get; set; }
        public DateTime? RetestDate { get; set; }
        public string QCRefNo { get; set; }
        public string QtyWithPackSize { get; set; }
        public string PrevQcReferenceNo { get; set; }
    }
}
