using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class ApprovalTypeViewModel
    {
        public int approvalTypeId { get; set; }
        public string approvalTypeName { get; set; }
        public bool? isActive { get; set; }
    }
}
