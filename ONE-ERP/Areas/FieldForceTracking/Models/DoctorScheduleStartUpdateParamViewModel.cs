using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistScheduleStartUpdateParamViewModel
    {
        public int PlanID { get; set; }



        public string Latitude { get; set; }
        public string Longitude { get; set; }


        public string startTime { get; set; }
    }
}
