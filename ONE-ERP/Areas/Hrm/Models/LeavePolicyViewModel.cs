using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class LeavePolicyViewModel
    {
        public int leavePolicyId { get; set; }
        public int? leaveTypeId { get; set; }
        public string typeName { get; set; }
        public int? yearId { get; set; }
        public string yearName { get; set; }
        public int? yearlyMaxLeave { get; set; }
        public int? yearlyMaxCarry { get; set; }
        public string remarks { get; set; }
        public bool? weeklyOffBridge { get; set; }
        public bool? govtHolidayBridge { get; set; }
        public string paymentType { get; set; }
        public int? highestCarryForward { get; set; }
        public int? maxBridgeLimit { get; set; }
        public bool? isActive { get; set; }
    }
}
