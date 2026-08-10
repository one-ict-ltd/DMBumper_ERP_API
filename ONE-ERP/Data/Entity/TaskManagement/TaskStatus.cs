using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.TaskManagement
{
    public class TaskStatus:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taskStatusId { get; set; }
        public string taskStatusName { get; set; }
        public string aliasName { get; set; }
        public int? sortOrder { get; set; }
    }
}
