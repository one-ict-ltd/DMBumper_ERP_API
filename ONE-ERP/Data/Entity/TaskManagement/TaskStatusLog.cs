using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.TaskManagement
{
    public class TaskStatusLog:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taskStatusLogId { get; set; }
        public int? taskInfoId { get; set; }
        public TaskInfo taskInfo { get; set; }
        public int? taskStatusId { get; set; }
        public TaskStatus taskStatus { get; set; }
        public DateTime? date { get; set; }
        public string remarks { get; set; }
    }
}
