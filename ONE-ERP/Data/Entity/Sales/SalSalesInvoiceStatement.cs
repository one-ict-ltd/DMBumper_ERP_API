using ONEERP.Data.Entity.Accounting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesInvoiceStatement : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int invoiceStatementId { get; set; }
        public int salesInvoiceId { get; set; }
        public int partyId { get; set; }
        public decimal? salesAmount { get; set; }
        public DateTime? matureDate { get; set; }
        public decimal? totalAdjustment { get; set; }
        public decimal? finalDues { get; set; }
        public bool isOverdue { get; set; }
        public DateTime? lastAdjustmentDate { get; set; }
        public string salesInvoiceNo { get; set; }
        public DateTime? salesInvoiceDate { get; set; }
        public int partyTypeId { get; set; }
        public decimal? salesReturnAmount { get; set; }
        public decimal? netSalesAmount { get; set; }
    }
}
