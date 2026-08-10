using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesInvoiceApproveViewModel
    {
        public int? salesInvoiceId { get; set; }        
        public List<SalesInvoiceApproveViewModel> data { get; set; }        
        public int status { get; set; } //0='Pending', 1='Approve', 2='Rejected/Cancelled', 3='Shipped', 4='Received', 5='OnHold', 6='Refund'

    }
}
