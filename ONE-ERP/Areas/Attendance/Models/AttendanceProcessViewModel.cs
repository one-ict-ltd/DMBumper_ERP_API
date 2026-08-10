using System;

namespace ONEERP.Areas.Attendance.Models
{
    public class AttendanceProcessViewModel
    {
        public int companyId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }        
       
    }
    public class AttLog
    {
        public int? Device_ID { get; set; }
        public string User_ID { get; set; }
        public string Verify_Date { get; set; }
        public int? Verify_Type { get; set; }
        public int? Verify_State { get; set; }
        public int? Work_Code { get; set; }

        //public int? Device_ID { get; set; }
        //public int? User_ID { get; set; }
        //public DateTime? Verify_Date { get; set; }
        //public int? Verify_Type { get; set; }
        //public int? Verify_State { get; set; }
        //public int? Work_Code { get; set; }
    }
}
