using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class UpdatePostingViewModel
    {
        public string EmergencyContact { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string ZoneId { get; set; }
        public string DepoId { get; set; }
        public string RegionId { get; set; }
        public string AreaId { get; set; }
        public string TerritoryId { get; set; }
        public string PostingLocation { get; set; }
        public string SalaryLocation { get; set; }
        public string Locationtxt { get; set; }
        public string Zonetxt { get; set; }
        public string Regiontxt { get; set; }
        public string Depottxt { get; set; }
        public string Areatxt { get; set; }
        public string Territorytxt { get; set; }
    }
}
