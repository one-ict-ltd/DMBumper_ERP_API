using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class MarketSetParamViewModel
    {
       
        public string Id { get; set; }
        public string MarketId { get; set; }
    
        public string Name { get; set; }
        
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }


    }
}
