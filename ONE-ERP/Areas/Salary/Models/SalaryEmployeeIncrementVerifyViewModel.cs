using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Salary.Models
{
    public class SalaryEmployeeIncrementVerifyViewModel
    {
        public int EmpFixedHeadStructureId { get; set; }
        public int EmpSalaryStructureId { get; set; }
        public int? employeeId { get; set; }
        public int? salaryGradeId { get; set; }
        public int? salarySlabId { get; set; }
        public decimal? increment { get; set; }
        public decimal? structureAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public bool? isActive { get; set; }
        public decimal? grossSalary { get; set; }

        //start Optional property
        public string employeeNo { get; set; }
        public string employeeName { get; set; }
        public string salaryGrade { get; set; }
        public string salaryPeriod { get; set; }
        public string status { get; set; }


    }
}
