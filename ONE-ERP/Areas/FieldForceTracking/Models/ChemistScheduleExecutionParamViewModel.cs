using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistScheduleExecutionParamViewModel
    {
   
        public int RosterID { get; set; }
        public int ChemistID { get; set; }
        public int MarketScheduleID { get; set; }
        public IFormFile ImageUrl { get; set; }
        public DateTime visitDate { get; set; }
        public string VisitTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Remarks { get; set; }
        public string LLAddress { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? CollectionAmount { get; set; }

     

      

    }
}
