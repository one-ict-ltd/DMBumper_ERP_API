using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class RMRequisitionDetailsViewModel
    {
        public int? requisitionDetailId { get; set; }
                   
        public decimal? qty { get; set; }
        public decimal? totalQty { get; set; }
        public int? productWiseSpecificationId { get; set; }

    }
}
