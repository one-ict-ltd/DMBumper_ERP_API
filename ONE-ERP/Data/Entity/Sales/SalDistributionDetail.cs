using ONEERP.Data.Entity.Accounting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalDistributionDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int distributionDetailId { get; set; }       
        public int? distributionMasterId { get; set; }
        public SalDistributionMaster distributionMaster { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salesInvoice { get; set; }
        public int? salesInvDetailsId { get; set; }
        public SalSalesInvoiceDetails salesInvoiceDetails { get; set; }
        public int? invoiceQty { get; set; }
        public int? distributionQty { get; set; }
    }
}
