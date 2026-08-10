using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesOrderStatusUpdateViewModel
    {
        public int salesInvoiceId { get; set; }
        public int statusId { get; set; }
    }
}
