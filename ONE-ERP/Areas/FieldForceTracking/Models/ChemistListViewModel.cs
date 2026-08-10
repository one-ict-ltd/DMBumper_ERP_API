using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistListViewModel
    {
        public int chemistID { get; set; }
        public string chemistNo { get; set; }
        public string chemistname { get; set; }
        public string mobileno { get; set; }
        public string telephoneno { get; set; }
        public string address { get; set; }
        public string druglicense { get; set; }
        public string creditlimit { get; set; }
        public string credit_days { get; set; }
        public int? isScheduled { get; set; }
        public int? partyTypeId { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string propritor { get; set; }
        public string marketName { get; set; }
        public string ownerName { get; set; }
        public bool? isActive { get; set; }
        //public string territoryId { get; set; }
        //public int marketId { get; set; }

        public string zoneId { get; set; }
        public string depoId { get; set; }
        public string regionId { get; set; }
        public string areaId { get; set; }
        public string territoryid { get; set; }
        public string marketId { get; set; }

        public IEnumerable<CmnChemist> Chemists { get; set; }
        public IEnumerable<AspNetUsersViewModel> Users { get; set; }
        public IEnumerable<ZoneListViewModel> Zones { get; set; }
        public IEnumerable<DepoListViewModel> Depos { get; set; }
        public IEnumerable<RegionListViewModel> Regions { get; set; }
        public IEnumerable<AreaListViewModel> Areas { get; set; }
        public IEnumerable<TeritoryListViewModel> Teritories { get; set; }
        public IEnumerable<MarketListViewModel> Markets { get; set; }
    }
}
