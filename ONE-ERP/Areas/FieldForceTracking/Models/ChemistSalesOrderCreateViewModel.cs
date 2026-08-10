using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistSalesOrderCreateViewModel
    {
        public DateTime? visitDate { get; set; }
        public int? chemistId { get; set; }
        public int? salesInvoiceId { get; set; }
        public string orderType { get; set; }
        public List<ProductSubCatGetViewModel> OrderDetails { get; set; }

    }
}
