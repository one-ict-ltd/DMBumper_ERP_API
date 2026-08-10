using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class NoteMasterViewModel
    {
        public int? noteMasterId { get; set; }
        public int? noteParentId { get; set; }       
        public string noteName { get; set; }
        public string noteNo { get; set; }
        public int? sortOrder { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }

    }
}
