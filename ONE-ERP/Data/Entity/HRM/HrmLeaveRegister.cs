using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeaveRegister:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveRegisterId { get; set; }

        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }

        public int? substituteEmployeeId { get; set; }
        public HrmEmployee substituteEmployee { get; set; }
        public int? leaveTypeId { get; set; }
        public HrmLeaveType leaveType { get; set; }
        public int? yearId { get; set; }
        public HrmLeaveYear year { get; set; }

        public int? leaveDay { get; set; }

        public int? leaveStatus { get; set; } //1 = approved || 2 = Rejected 

        public int? type { get; set; } //1=pre leave | 2 = post leave

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public string leaveLocation { get; set; }
        public string emergencyContact { get; set; }
        public string remarks { get; set; }
    }
}
