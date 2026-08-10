using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Models
{
    public class ShiftGroupMasterViewModel
    {
        public int shiftMasterId { get; set; }
        public string shiftName { get; set; }
        public bool? isActive { get; set; }
        public int? isDetailsUpdated { get; set; }
        public List<ShiftGroupDetailViewModel> lstDetails { get; set; }
    }
}
