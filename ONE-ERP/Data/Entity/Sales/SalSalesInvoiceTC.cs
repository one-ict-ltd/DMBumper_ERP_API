using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesInvoiceTC : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? salesInvoiceTCId { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salesInvoice{get;set;}
        public string termsAndCondition { get; set; }

        public int? salesOrderTCId { get; set; }
        public SalSalesOrderTC salesOrderTC { get; set; }
    }
    public class SalSalesOrderTC : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? salesOrderTCId { get; set; }
        public int salesOrderId { get; set; }
        public SalSalesOrder salesOrder { get; set; }
        public string termsAndCondition { get; set; }
    }
}
