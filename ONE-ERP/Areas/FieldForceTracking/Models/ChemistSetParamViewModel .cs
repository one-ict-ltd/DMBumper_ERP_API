using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistSetParamViewModel
    {
       
        public string Id { get; set; }
        public int ChemistID { get; set; }
        public string ChemistName { get; set; }
        public string ChemistType { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MobileNo { get; set; }
        public string Propritor { get; set; }
        public string MarketCode { get; set; }

    }
}
