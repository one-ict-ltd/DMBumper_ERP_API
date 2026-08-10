using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmCoreFunction : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int coreFunctionId { get; set; }
        public int? departmentId { get; set; }
        public HrmDepartment department { get; set; }
        public string functionName { get; set; }
    }
}
