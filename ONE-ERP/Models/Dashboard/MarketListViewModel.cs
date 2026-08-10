using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models.Dashboard
{
    public class MarketListViewModel 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string TerritoryCode { get; set; }
        public string AreaCode { get; set; }
        public string RegionCode { get; set; }
        public string DepotCode { get; set; }
        public string ZoneCode { get; set; }
        public bool IsActive { get; set; }
     
    }
}
