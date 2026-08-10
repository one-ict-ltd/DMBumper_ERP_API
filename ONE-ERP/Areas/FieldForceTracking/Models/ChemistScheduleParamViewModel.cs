using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistScheduleParamViewModel
    {     
        public int RosterID { get; set; }
        public int ChemistID { get; set; }
        public string visitDate { get; set; }
        public string VisitTime { get; set; }
        public string Opinion { get; set; }

    }
}
