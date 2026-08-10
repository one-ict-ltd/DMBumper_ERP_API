using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PortInfoViewModel
    {
        public int portInfoId { get; set; }
        public string portInfoName { get; set; }
        public string portInfoCode { get; set; }
        public string shortName { get; set; }
        public int? shortOrder { get; set; }
        public bool isActive { get; set; }
    }
}


