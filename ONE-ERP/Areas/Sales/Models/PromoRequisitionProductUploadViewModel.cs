using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class PromoRequisitionProductUploadViewModel
    {
        public string program { get; set; }
        public string allocationTypeId { get; set; }
        public List<ListModelForPromoRequisition> lstDetailsViewModel { get; set; }
    }
}
