using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class ListModelForPromoRequisition
    {
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public string areaManagerCode { get; set; }
        public string productCode { get; set; }
        public decimal? quantity { get; set; }


    }
}
