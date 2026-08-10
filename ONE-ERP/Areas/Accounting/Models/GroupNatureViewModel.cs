using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class GroupNatureViewModel
    {
        public int? groupNatureId { get; set; }
        public string natureName { get; set; }
        public int? printOrder { get; set; }
        public bool? isActive { get; set; }

    }
}
