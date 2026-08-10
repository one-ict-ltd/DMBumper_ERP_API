using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Areas.Auth.Models
{
  
    public class UserAccessPageListViewModel
    {
        public int? userAccessPageId { get; set; }

        public int? navbarId { get; set; }

        public int? isAccess { get; set; }

        public string applicationRoleId { get; set; }

        public int? companyId { get; set; }

        public int? sbuId { get; set; }

        public int? isActive { get; set; }
    }
}
