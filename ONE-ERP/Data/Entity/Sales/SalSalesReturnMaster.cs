using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesReturnMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesReturnMasterId { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salesInvoice { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? storeId { get; set; }
        public CmnStore store { get; set; }

        [MaxLength(100)]
        public string salesReturnNo { get; set; }
        public DateTime? salesReturnDate { get; set; }  
        public decimal? grossAmount { get; set; }
        public decimal? totalVatAmount { get; set; }
        public decimal? totalAitAmount { get; set; }
        public decimal? shippingCostAmount { get; set; }
        public decimal? totalDiscountAmount { get; set; }
        public decimal? netAmount { get; set; }
        public string comments { get; set; }
        public bool? isClose { get; set; }
    }
}
