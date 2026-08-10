using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnModulePermissions:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int modulePermissionId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? moduleId { get; set; }
        public CmnModule module { get; set; }
        public int? defaultMenuId { get; set; }
    }
}
