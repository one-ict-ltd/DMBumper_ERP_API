using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.TaskManagement
{
    public class TaskTeamDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taskTeamDetailId { get; set; }
        public int? taskTeamMasterId { get; set; }
        public TaskTeamMaster taskTeamMaster { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }

    }
}
