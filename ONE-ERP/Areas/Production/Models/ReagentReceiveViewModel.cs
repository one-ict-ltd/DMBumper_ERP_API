using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ReagentReceiveViewModel
    {
        public int reagentReceiveMasterId { get; set; }
        public string receiveNo { get; set; }
        public DateTime receiveDate { get; set; }
        public string typeOfreceive { get; set; }
        public int? reagentIssueMasterId { get; set; }
        public decimal? receiveQty { get; set; }
        public int? receiveStatus { get; set; }
        public string receiveRemarks { get; set; }
        public int? bomForId { get; set; }

        public List<ReagentReceiveDetailViewModel> lstDetailsViewModel { get; set; }
    }
}
