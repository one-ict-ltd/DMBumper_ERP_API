using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Models
{
    public class CalenderViewModel
    {  
        public int Day { get; set; }
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public int MonthNo { get; set; }
        public int Year { get; set; }
        //public int? IsHoliDay { get; set; }       
        public bool? isActive { get; set; }
        public List<CalenderViewModel> lstModel { get; set; }

    }

    public class ManualAttendanceViewModel
    {
        public int manualAttendanceId { get; set; }
        public int? employeeId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string remarks { get; set; }
        public DateTime? applicationDate { get; set; }
        public int? workingTime { get; set; }
    }
}
