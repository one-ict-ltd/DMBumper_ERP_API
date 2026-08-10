using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class LeaveRegisterViewModel
    {
        public int? leaveRegisterId { get; set; }
        public int? leaveApprovalLogId { get; set; }

        public int? employeeId { get; set; }

        public int? substituteEmployeeId { get; set; }
        public int? leaveTypeId { get; set; }
        public int? yearId { get; set; }

        public int? leaveDay { get; set; }

        public int? leaveStatus { get; set; } //0 = ongoing || 1 = approved || 2 = rejected

        public int? type { get; set; } //1=pre leave | 2 = post leave

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public string leaveLocation { get; set; }
        public string emergencyContact { get; set; }
        public string remarks { get; set; }
        public string comments { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public List<LeaveRegisterViewModel> lstMasterViewModel { get; set; }
    }

    public class HrmLateAttandaceVM{
        public int? leaveStatus { get; set; }
        public List<HrmLateAttandanceViewModel> lstMasterViewModel { get; set; }
    }
    public class HrmLateAttandanceViewModel
    {
        public int? attandanceClarificationId { get; set; }
        public int? lateAttandanceApprovalLog { get; set; }

        public int? employeeId { get; set; }
        public int? lateStatus { get; set; } //0 = ongoing || 1 = approved || 2 = rejected
        public DateTime? lateDate { get; set; }
       
        public string remarks { get; set; }
        public string comments { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
    }
}
