using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class GroupNatureListViewModel
    {
        public int? groupNatureId { get; set; }
        public string natureName { get; set; }
        public int? printOrder { get; set; }
        public int? isActive { get; set; }
        public string status { get; set; }
    }
}
