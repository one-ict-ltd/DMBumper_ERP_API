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
    public class HrmEmployeeTeam : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeTeamId { get; set; }
        public int? teamLeadEmployeeId { get; set; }
        public HrmEmployee teamLeadEmployee { get; set; }
        public int? teamMemberEmployeeId { get; set; }
        public HrmEmployee teamMemberEmployee { get; set; }

    }
}
