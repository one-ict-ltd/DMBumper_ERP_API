using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Models
{
    public class ShiftGroupDetailViewModel
    {        
        public string weekDay { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public bool? isHoliday { get; set; }
        public bool? isActive { get; set; }
    }
}
