using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class RequisitionFinalMasterViewModel
    {
        public int requisitionFinalizeMasterId { get; set; }
        
        public string finalRequsitionNo { get; set; }

        public string requisitionFianlDate { get; set; }

        public string remarks { get; set; }

        public int status { get; set; }

        public List<RequisitionFinalMasterDetailViewmodel> lstApproveReqViewModel { get; set; }
    }
}
