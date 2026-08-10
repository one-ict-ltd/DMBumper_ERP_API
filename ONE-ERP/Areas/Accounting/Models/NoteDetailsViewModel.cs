using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class NoteDetailsViewModel
    {
        public int? noteDetailsId { get; set; }
        public int? noteMasterId { get; set; }
        public int? ledgerId { get; set; }
        public int? sortOrder { get; set; }
        public bool? isActive { get; set; }

    }
}
