using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class StockSalesChartViewModel
    {
       
        public string BrandName { get; set; }
        public int StockQty { get; set; }
        public int SaleQty { get; set; }
        public string ColorCode { get; set; }
    }
}
