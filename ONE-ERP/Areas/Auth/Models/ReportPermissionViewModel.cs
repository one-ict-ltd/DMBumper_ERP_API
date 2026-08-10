using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Auth.Models
{
    public class ReportPermissionViewModel
    {        
        public int? reportPermissionId { get; set; }
        public int? reportId { get; set; }
        public int? employeeId { get; set; }       
        public bool? isActive { get; set; }

        public List<ReportPermissionViewModel> lstModel { get; set; }

    }
}
