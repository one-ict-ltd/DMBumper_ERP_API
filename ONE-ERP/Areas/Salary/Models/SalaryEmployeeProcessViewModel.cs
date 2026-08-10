using System;

namespace ONEERP.Areas.Salary.Models
{
    public class SalaryEmployeeProcessViewModel
    {
        public int salaryPeriodId { get; set; }

        //Process Log
        public string processName { get; set; }
        public string processComments { get; set; }
        public string ipAddress { get; set; }

    }
}
