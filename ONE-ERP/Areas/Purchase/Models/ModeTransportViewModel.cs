using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ModeTransportViewModel
    {
        public int modeTransportId { get; set; }
        public string modeTransportName { get; set; }
        public string modeTransportCode { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public bool isActive { get; set; }
    }
}
