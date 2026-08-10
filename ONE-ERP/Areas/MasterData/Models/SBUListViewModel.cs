using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class SBUListViewModel
    {
        public int? sbuId { get; set; }
        public string sbuName { get; set; }
        public string aliasName { get; set; }
        public string sbuCode { get; set; }
        public int? shortOrder { get; set; }
        public bool? isDefault { get; set; }
        public int? companyId { get; set; }
        public string companyName { get; set; }
        public bool? isActive { get; set; }
    }
}
