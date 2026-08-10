using System;

namespace ONEERP.Areas.Auth.Models
{
    public class AspNetUsersProfileViewModel
    {
        //E.EMP_ID MIOCode, E.EMPLOYEE_NAME MIOName, T.TerritoryName,A.AreaName,Re.RegionName,De.DepotName,ZoneName,POSTING_LOCATION
        public int? EmployeeId { get; set; }
        public string MIOCode { get; set; }
        public string MIOName { get; set; }
        public string TerritoryName { get; set; }
        public string TerritoryCode { get; set; }
        public string AreaName { get; set; }
        public string RegionName { get; set; }
        public string DepotName { get; set; }
        public string ZoneName { get; set; }
        public string POSTING_LOCATION { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public int TotalDoctor { get; set; }
        public int TotalChemist { get; set; }
        public int TotalCustomer { get; set; }
        public int TotalMIO { get; set; }
        public string SupportName { get; set; }
        public string SupportMobile { get; set; }
        public string ASMCode { get; set; }
        public string RSMCode { get; set; }
        public int? companyId { get; set; }
        public string deviceNo { get; set; }
    }
}
