using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class LocalAgentViewModel
    {
        public int localAgentId { get; set; }
        public string localAgentName { get; set; }
        public string localAgentCode { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public bool isActive { get; set; }
    }
}
