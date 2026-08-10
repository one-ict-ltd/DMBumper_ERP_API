using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DoctorWiseVisitReportViewModel
    {

        public int DoctorID { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }

        public string opinion { get; set; }
        public string remarks { get; set; }
        public string visitDateTime { get; set; }
        public string visitedDateTime { get; set; }
        public string rosterName { get; set; }
        public string lladdress { get; set; }
        public string imageUrl { get; set; }




    }
}
