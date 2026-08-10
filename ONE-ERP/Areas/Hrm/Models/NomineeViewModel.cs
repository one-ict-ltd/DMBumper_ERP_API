//
//
using ONEERP.Areas.Hrm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class NomineeViewModel
    {
        public int? employeeID { get; set; }
        public int? nomineeID { get; set; }
        public string name { get; set; }
        public string relation { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public int?[] fundTypeList { get; set; }
        public decimal?[] fundValueList { get; set; }

        public string employeeNameCode { get; set; }
        public string guardianName { get; set; }
        public string witnessName { get; set; }
        public DateTime? nomineeDate { get; set; }

        public PhotographViewModel photograph { get; set; }
        public EmployeeViewModel employeeInfo { get; set; }

        //public IEnumerable<NomineeFund> nomineeFunds { get; set; }
        //public IEnumerable<NomineeDetail> nomineeDetails { get; set; }
        //public IEnumerable<Nominee> nominees { get; set; }
    }
}
