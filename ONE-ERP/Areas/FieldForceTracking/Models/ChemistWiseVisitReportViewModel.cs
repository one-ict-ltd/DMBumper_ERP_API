using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistWiseVisitReportViewModel
    {
        public int ChemistID { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        
        public string opinion { get; set; }
        public string remarks { get; set; }
        public string visitDateTime { get; set; }
        public string date { get; set; }
        public string visitedDateTime { get; set; }
        public string rosterName { get; set; }
        public string address { get; set; }
        public string imageUrl { get; set; }      
        public decimal? collectionAmount { get; set; }
        public decimal? invoiceAmount { get; set; }


    }
}
