using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class MarketScheduleParamViewModel
    {
   
        public int RosterID { get; set; }
        public int MarketID { get; set; }
        public string visitDate { get; set; }
        public string VisitTime { get; set; }
        public string Opinion { get; set; }
        public string ZoneCode { get; set; }
        public string DepotCode { get; set; }
        public string RegionCode { get; set; }
        public string AreaCode { get; set; }
        public string TerritoryCode { get; set; }
        public string MioCode { get; set; }

    }
}
