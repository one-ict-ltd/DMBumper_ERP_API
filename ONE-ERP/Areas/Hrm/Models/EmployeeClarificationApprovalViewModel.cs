using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeClarificationApprovalViewModel
    {
        public int employeecClarificationId { get; set; }
        public int Status { get; set; }
        public int EmployeeClarificationLogId { get; set; }
        public bool? isSelect { get; set; }
        public string comments { get; set; }
        public List<EmployeeClarificationApprovalViewModel> lstMasterViewModel { get; set; }
    }
}
