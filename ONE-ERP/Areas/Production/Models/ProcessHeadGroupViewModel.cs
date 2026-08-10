using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class ProcessHeadGroupViewModel
    {
        public int phGroupMasterId { get; set; }
        public int? productionTypeId { get; set; } // 1= Menufacturing,2= Packing
        public string groupName { get; set; }
        public List<ProcessHeadGroupDetailsViewModel> lstDetailsViewModel { get; set; }
    }

    public class ProcessHeadGroupDetailsViewModel
    {
        public int phGroupDetailId { get; set; }
        public int? phGroupMasterId { get; set; }
        public int? processHeadId { get; set; }
        public int? headOrder { get; set; }
        public int? isQA { get; set; }
        public string remarks { get; set; }
        public int? hasQC { get; set; }
    }
}
