using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class BomFinishGoodStockInDetailsViewModel
    {
        public int? bomStockInDetailsId { get; set; }
        public int? bomStockInId { get; set; }
        public int? bomId { get; set; }
        public decimal? qty { get; set; }
        public int? isSelect{ get; set; }
    }
}
