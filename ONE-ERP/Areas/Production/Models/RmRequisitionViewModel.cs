using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class RmRequisitionViewModel
    {
        public int? rmRequisitonId { get; set; }
        public int? productionPlanId { get; set; }
        public string reqNo { get; set; }
        public DateTime? reqDate { get; set; }
        public int? bomQty { get; set; }
        public string type { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomId { get; set; }
        public int? bomMasterProductWiseSpecificationId { get; set; }
        public int? bomForId { get; set; }
        public List<RMRequisitionDetailsViewModel> lstDetailsViewModel { get; set; }     
    }
}
