using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeavePolicy : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leavePolicyId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? leaveTypeId { get; set; }
        public HrmLeaveType leaveType { get; set; }
        public int? yearId { get; set; }
        public HrmLeaveYear year { get; set; }
        public int? yearlyMaxLeave { get; set; }
        public int? yearlyMaxCarry { get; set; }
        public string remarks { get; set; }
        public bool? weeklyOffBridge { get; set; }
        public bool? govtHolidayBridge { get; set; }
        public string paymentType { get; set; }
        public int? highestCarryForward { get; set; }
        public int? maxBridgeLimit { get; set; }
    }
}
