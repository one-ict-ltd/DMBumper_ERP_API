using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeInfoUploadVerifyViewModel
    {
      
        public int? employeeId { get; set; }
        public bool? isActive { get; set; }
        public string employeeNo { get; set; }
        public string employeeName { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string joiningdate { get; set; }
        //public string DateString { get; set; }
        public string contractNumber { get; set; }
        public string regionCode { get; set; }
        public string regionName { get; set; }
        public string areaCode { get; set; }
        public string areaName { get; set; }
        public string territoryCode { get; set; }
        public string territoryName { get; set; }
        public int? depotCode { get; set; }
        public string depotName { get; set; }
        public string postingType { get; set; }
        public string salaryLocation { get; set; }
        public string salaryDepot { get; set; }
        public string status { get; set; }
    }
}
