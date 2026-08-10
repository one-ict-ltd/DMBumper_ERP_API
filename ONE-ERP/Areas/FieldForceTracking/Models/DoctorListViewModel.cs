using ONEERP.Areas.Auth.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models.Dashboard;
using System.Collections.Generic;

namespace ONEERP.Areas.MasterData.Models
{
    public class DoctorListViewModel
    {
        public int doctorId { get; set; }
        public string doctorNo { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public int? isScheduled { get; set; }
        public string speciality { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string degree { get; set; }
        public string designation { get; set; }
        public string institude { get; set; }
        public string noOfPatient { get; set; }
        public int isActive { get; set; }
        //public string territoryId { get; set; }
        //public int marketId { get; set; }
        public string institute { get; set; }

        public string zoneId { get; set; }
        public string depoId { get; set; }
        public string regionId { get; set; }
        public string areaId { get; set; }
        public string territoryId { get; set; }
        public string marketId { get; set; }

        public IEnumerable<CmnDoctor> Doctors { get; set; }
        public IEnumerable<AspNetUsersViewModel> Users { get; set; }
        public IEnumerable<ZoneListViewModel> Zones { get; set; }
        public IEnumerable<DepoListViewModel> Depos { get; set; }
        public IEnumerable<RegionListViewModel> Regions { get; set; }
        public IEnumerable<AreaListViewModel> Areas { get; set; }
        public IEnumerable<TeritoryListViewModel> Teritories { get; set; }
        public IEnumerable<MarketListViewModel> Markets { get; set; }
    }
}
