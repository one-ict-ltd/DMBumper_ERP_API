using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models.Dashboard
{
    public class MIOCurrentLocationViewModel
    {
        public string MIOCode { get; set; }
        public string MIOName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LLAddress { get; set; }
        public DateTime? DateTime { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public int isIO { get; set; }
        //public string ViewMap { get; set; }
    }
    public class MIOCurrentLocationNNViewModel
    {
        public string MIOCode { get; set; }
        public string MIOName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LLAddress { get; set; }
        //public DateTime? DateTime { get; set; }
        public string DateTime { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string ViewMap { get; set; }
     


    }
}
