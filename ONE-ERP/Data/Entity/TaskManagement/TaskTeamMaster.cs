using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.TaskManagement
{
    public class TaskTeamMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taskTeamMasterId { get; set; }
        public int? teamLeaderId { get; set; }
        public HrmEmployee teamLeader { get; set; }
        public string teamName { get; set; }
        public string teamCode { get; set; }
        public string description { get; set; }
    }
}
