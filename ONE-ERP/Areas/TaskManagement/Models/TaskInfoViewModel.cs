using System;
using System.Collections.Generic;

namespace ONEERP.Areas.TaskManagement.Models
{
    public class TaskInfoViewModel
    {
        public int? taskInfoId { get; set; }
        public string taskName { get; set; }
        public string description { get; set; }
        public int? taskCode { get; set; }
        public int? taskTypeId { get; set; }
        public int? employeeId { get; set; }
        public int? assignToId { get; set; }
        public int? taskPriorityId { get; set; }
        public DateTime? date { get; set; }
        //public string atime { get; set; }
        public DateTime? expectedEndDate { get; set; }
        //public string etime { get; set; }
        public bool? isParent { get; set; }
        public int? parentTaskId { get; set; }
        public bool? isActive { get; set; }
        public List<TaskInfoViewModel> lstTaskInfoViewModel { get; set; }
    }

    public class TaskStatusLogViewModel
    {
        public int? taskInfoId { get; set; }
        public int? taskStatusId { get; set; }
        public string remarks { get; set; }
        public string time { get; set; }
        public DateTime? date { get; set; }
        public bool? isActive { get; set; }
    }

    public class TaskTeamViewModel
    {
        public int? taskTeamMasterId { get; set; }
        public int? teamLeaderId { get; set; }
        public string teamName { get; set; }
        public string teamCode { get; set; }
        public string description { get; set; }
        public bool? isActive { get; set; }
        public List<TaskTeamDetailsViewModel> lstTaskTeamDetails { get; set; }
    }

    public class TaskTeamDetailsViewModel
    {
        public int? taskTeamDetailId { get; set; }
        public int? taskTeamMasterId { get; set; }
        public int? employeeId { get; set; }
        public bool? isActive { get; set; }
    }
}
