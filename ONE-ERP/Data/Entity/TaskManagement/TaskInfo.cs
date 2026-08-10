using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.TaskManagement
{
    public class TaskInfo:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taskInfoId { get; set; }
        public string taskName { get; set; }
        public string taskCode { get; set; }
        public string description { get; set; }
        public int? taskTypeId { get; set; }
        public TaskType taskType { get; set; }
        public int? employeeId { get; set; }
        public int? assignToId { get; set; }
        public HrmEmployee assignTo { get; set; }
        public int? taskPriorityId { get; set; }
        public TaskPriority taskPriority { get; set; }
        public DateTime? date { get; set; }
        public DateTime? expectedEndDate { get; set; }
        public int? isParent { get; set; }
        public int? parentTaskId { get; set; }
        public TaskInfo parentTask { get; set; }
    }
}
