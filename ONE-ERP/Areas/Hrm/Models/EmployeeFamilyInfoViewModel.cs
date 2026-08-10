using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeFamilyInfoViewModel
    {
        public int? familyInfoId { get; set; }
        public int? employeeId { get; set; }
        public int? relationId { get; set; }
        public DateTime? dob { get; set; }
        public string name { get; set; }
        public string occupation { get; set; }
        public string mobile { get; set; }
        public string NID { get; set; }
        public string passport { get; set; }
        public string email { get; set; }
        public string remarks { get; set; }
    }
}
