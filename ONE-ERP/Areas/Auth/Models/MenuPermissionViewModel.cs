using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Auth.Models
{
    public class MenuPermissionViewModel
    {
        public int? menuPermissionId { get; set; }
        public int? menuId { get; set; }
        public int? moduleId { get; set; }
        public int? userGroupId { get; set; }
        public int? employeeId { get; set; }
        public int? companyId { get; set; }        
        public DateTime? effectiveDate { get; set; }
        public bool? enableView { get; set; }
        public bool? enableInsert { get; set; }
        public bool? enableUpdate { get; set; }
        public bool? enableDelete { get; set; }
        public bool? isActive { get; set; }
        public List<MenuPermissionViewModel> lstModel { get; set; }
    }
}
