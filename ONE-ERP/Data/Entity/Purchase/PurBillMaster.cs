using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurBillMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int billMasterId { get; set; }
        [MaxLength(30)]
        public string billNo { get; set; }
        public DateTime? billDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public string omrRmrNo { get; set; }
        public DateTime? omrRmrDate { get; set; }
        
        public string supplierBillNo { get; set; }
        public DateTime? supplierBillDate { get; set; }
        public string supplierChallanNo { get; set; }
        public DateTime? supplierChallanDate { get; set; }

        public DateTime? maturityDate { get; set; }
        public int? creditPeriod { get; set; }

        public string particular { get; set; }
        public string remarks { get; set; }

        public decimal? grandTotal { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? truckFair { get; set; }
        public decimal? transportBill { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? vdsPercent { get; set; }
        public decimal? vdsAmount { get; set; }
        public decimal? tdsPercent { get; set; }
        public decimal? tdsAmount { get; set; }
        public decimal? advancePaidAmount { get; set; }
        public decimal? netAmount { get; set; }
        public int? billStatus { get; set; }

    }
}
