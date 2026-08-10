using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Auth.Models
{
    public class ModulePermissionsViewModel
    {        
        public int? modulePermissionId { get; set; }
        public int? moduleId { get; set; }
        public int? companyId { get; set; }
        public int? defaultMenuId { get; set; }
        public bool? isActive { get; set; }
        public List<ModulePermissionsViewModel> lstModel { get; set; }

    }
}
