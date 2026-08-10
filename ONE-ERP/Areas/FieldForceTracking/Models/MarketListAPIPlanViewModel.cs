using System.Collections.Generic;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class MarketListAPIPlanViewModel
    {
        public int MarketId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int IsScheduled { get; set; }
    }
}
