using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistScheduleListViewModel
    {

        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string remarks { get; set; }
        public string visitDateTime { get; set; }
        public string rosterName { get; set; }
        public int isexecuted { get; set; }
        public string startTime { get; set; }
        public int status { get; set; }

    }
}
