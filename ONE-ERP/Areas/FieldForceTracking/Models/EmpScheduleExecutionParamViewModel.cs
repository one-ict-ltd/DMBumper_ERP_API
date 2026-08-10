using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class EmpScheduleExecutionParamViewModel
    {
   
        public int RosterID { get; set; }
        public string EmpCode { get; set; }
        public int MarketScheduleID { get; set; }
        public IFormFile ImageUrl { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Remarks { get; set; }
        public string LLAddress { get; set; }

     

      

    }
}
