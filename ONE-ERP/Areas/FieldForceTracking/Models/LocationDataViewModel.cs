using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class LocationDataViewModel
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string visitDateTime { get; set; }
    }
}
