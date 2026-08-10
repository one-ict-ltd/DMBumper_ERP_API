using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesInvoiceTandCViewModel
    {
        public int? salesInvoiceTCId { get; set; }
        public int? salesInvoiceId { get; set; }
        public string termsAndCondition { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
    }
    public class SalesOrderTandCViewModel
    {
        public int? salesOrderTCId { get; set; }
        public int? salesOrderId { get; set; }
        public string termsAndCondition { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
    }
}
