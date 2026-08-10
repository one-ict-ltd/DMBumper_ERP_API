using System;
using System.Collections.Generic;

namespace ONEERP.Areas.TaskManagement.Models
{
    public class EmployeeWeeklyTaskAssignViewModel
    {
        public int employeeWeeklyTaskAssignId { get; set; }
        public int? teamLeadEmployeeId { get; set; }
        public int? teamMemberEmployeeId { get; set; }
        public int? departmentId { get; set; }
        public int? designationId { get; set; }
        public int? year { get; set; }
        public string month { get; set; }
        public string week { get; set; }
        public int? coreFunctionId { get; set; }
        public decimal? taskQty { get; set; }
        public string description { get; set; }
        public bool? isActive { get; set; }
        public decimal? completedTaskQty { get; set; }
        public string status { get; set; }
        public List<EmployeeWeeklyTaskAssignViewModel> lstDetails { get; set; }

    }
}
