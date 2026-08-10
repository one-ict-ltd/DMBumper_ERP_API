using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductionProcessHeadViewModel
    {
        public int processHeadId { get; set; }
        public int shortOrder { get; set; }
        public bool isActive { get; set; }
        public bool isQA { get; set; }
        public string shortName { get; set; }
        public string headName { get; set; }
        public string headCode { get; set; }
        public string description { get; set; }

    }

    
}
