using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnUserPermissionGroup:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userPermissionGroupId { get; set; }
        public int? userGroupId { get; set; }
        public CmnUserGroup userGroup { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
