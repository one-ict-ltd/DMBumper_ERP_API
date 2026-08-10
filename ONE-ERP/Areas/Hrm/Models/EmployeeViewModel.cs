using System;
using ONEERP.Models.Dashboard;
using System.Collections.Generic;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeViewModel  ///// DON'T CHNAGE IT
    {
        public int? employeeId { get; set; }
        public string employeeNo { get; set; }
        public int? employeeTypeId { get; set; }
        public string fullName { get; set; }
        public string FATHER_NAME { get; set; }
        public string PRESENT_ADD { get; set; }
        public string PERMANENT_ADD { get; set; }
        public string DESIGNATION { get; set; }
        public string MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string REMARKS { get; set; }
        public string EMP_STATUS { get; set; }
        public string BLOOD_GROUP { get; set; }
        public string NATIONAL_ID { get; set; }
        public string LAST_QUALIFICATION { get; set; }
        public string POSTING_LOCATION { get; set; }
        public string emailId { get; set; }
        public string ZONE_CODE { get; set; }
        public string ZoneName { get; set; }
        public string DEPOT_CODE { get; set; }
        public string DepotName { get; set; }
        public string REGION_CODE { get; set; }
        public string RegionName { get; set; }
        public string AREA_CODE { get; set; }
        public string AreaName { get; set; }
        public string TERRITORY_CODE { get; set; }
        public string TerritoryName { get; set; }
        public string EMP_TYPE { get; set; }
        public string Token { get; set; }
        public string DOB { get; set; }
        public string companyId { get; set; }
        public int? designationId { get; set; }
        public bool? isActive { get; set; }
        public DateTime? heldUpDate { get; set; }


        public IEnumerable<ZoneListViewModel> zoneListViewModels { get; set; }
        public IEnumerable<DepoListViewModel> depoListViewModels { get; set; }
        public IEnumerable<AreaListViewModel> areaListViewModels { get; set; }
        public IEnumerable<RegionListViewModel> regionListViewModels { get; set; }
        public IEnumerable<TeritoryListViewModel> teritoryListViewModels { get; set; }
        public IEnumerable<EmployeeViewModel> employeeViewModels { get; set; }
    }
  
}
