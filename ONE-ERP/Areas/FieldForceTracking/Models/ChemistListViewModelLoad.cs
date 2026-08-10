using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistListViewModelLoad
    {
        public int chemistid { get; set; }
        public string chemistno { get; set; }
        public string chemistname { get; set; }
        public int? partyTypeId { get; set; }
        public string partyTypeName { get; set; }
        //public string latitude { get; set; }
        //public string longitude { get; set; }
        public string mobileno { get; set; }
        //public string telephoneno { get; set; }
        public decimal? creditlimit { get; set; }
        //public int credit_days { get; set; }
        public string ownername { get; set; }
        //public string druglicense { get; set; }
        //public string MarketName { get; set; }
        public int? companyid { get; set; }
        public bool? isActive { get; set; }
        public int? isscheduled { get; set; }
        public string address { get; set; }
        //public string propritor { get; set; }
        //public string marketid { get; set; }
        public string territoryid { get; set; }
        public string territoryname { get; set; }
        public string areacode { get; set; }
        public string areaname { get; set; }
        public string regioncode { get; set; }
        public string regionname { get; set; }
        public string depotcode { get; set; }
        public string depotname { get; set; }
        public string zonecode { get; set; }
        public string zonename { get; set; }
       
    }
}
