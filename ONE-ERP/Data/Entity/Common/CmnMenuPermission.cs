using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnMenuPermission:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int menuPermissionId { get; set; }
        public int? menuId { get; set; }
        public CmnModule module { get; set; }
        public int? userGroupId { get; set; }
        public CmnUserGroup userGroup { get; set; }
        public int? employeeId { get; set; }        
        public int? companyId { get; set; }
        public DateTime? effectiveDate { get; set; }
        public bool? enableView { get; set; }
        public bool? enableInsert { get; set; }
        public bool? enableUpdate { get; set; }
        public bool? enableDelete { get; set; }
    }
}
