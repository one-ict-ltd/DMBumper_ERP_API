using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class InactiveEmployeeInfoUploadVerifyViewModel
    {
        public int? employeeNo { get; set; }
        public string inActiveDate { get; set; }
        public string status { get; set; }
    }
}
