
using System.Collections.Generic;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DoctorListAPIViewModel
    {
        public int doctorId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public int? isScheduled { get; set; }
        public string speciality { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string degree { get; set; }
        public string designation { get; set; }
        public string institude { get; set; }
        public string noOfPatient { get; set; }
        public string marketCode { get; set; }
       
    }
}
