using System.Collections.Generic;

namespace ONEERP.Areas.Auth.Models
{
    public class UserPermissionGroupViewModel
    {
        public int? userPermissionGroupId { get; set; }
        public int? userGroupId { get; set; }
        public int? employeeId { get; set; }
        public int? companyId { get; set; }
        public bool? isActive { get; set; }
        public List<UserPermissionGroupViewModel> lstModel { get; set; }
    }
}
