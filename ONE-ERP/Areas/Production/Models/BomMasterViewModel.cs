using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class BomMasterViewModel
    {
      
        public List<BomMasterModel> bomMaster { get; set; }

    }
    public class BomMasterModel
    {
        public int pendingbomId { get; set; }

        public int approvalStatus { get; set; }
    }

}
