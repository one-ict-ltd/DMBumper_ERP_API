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
    public class HrmEmployeeMonthlyTaskAssign : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeMonthlyTaskAssignId { get; set; }
        public int? teamLeadEmployeeId { get; set; }
        public HrmEmployee teamLeadEmployee { get; set; }
        public int? teamMemberEmployeeId { get; set; }
        public HrmEmployee teamMemberEmployee { get; set; }
        public int? departmentId { get; set; }
        public HrmDepartment department { get; set; }
        public int? designationId { get; set; }
        public HrmDesignation designation { get; set; }
        public int? year { get; set; }
        public string month { get; set; }
        public int? coreFunctionId { get; set; }
        public HrmCoreFunction coreFunction { get; set; }
        public decimal? taskQty { get; set; }
        public string description { get; set; }
    }
}
