namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistListAPIViewModel
    {       
        public int chemistID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }      
        public string address { get; set; }
        public int? isScheduled { get; set; }
        public string chemistType { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string propritor { get; set; }
        public string marketCode { get; set; }
        public string CategoryName { get; set; }
        public string TerritoryCode { get; set; }
    }
}
