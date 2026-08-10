using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class CheckInOutParamViewModel
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string dateTime { get; set; }
        public string flag { get; set; }
        public string address { get; set; }
        public string opinion { get; set; }
        public string time { get; set; }

        public bool? isHQ { get; set; }
        public bool? isEHQ { get; set; }
        public bool? isOS { get; set; }
        public bool? isOther { get; set; }
    }
}
