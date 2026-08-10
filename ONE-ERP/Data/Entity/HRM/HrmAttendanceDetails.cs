using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmAttendanceDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int detailsId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public DateTime? attendanceDate { get; set; }
        [MaxLength(20)]
        public string startTime { get; set; }
        [MaxLength(20)]
        public string endTime { get; set; }        
        public int? workingTime { get; set; }
        public int? latetime { get; set; }
        public int? statusId { get; set; } //See CmnDropDown Table for details 
        public string remarks { get; set; }
        [MaxLength(250)]
        public string attendanceUpdatedBy { get; set; }
        [MaxLength(250)]
        public string attendanceApprovedBy { get; set; }
        public int? shiftMasterId { get; set; }
        public HrmAttendanceShiftGroupMaster shiftMaster { get; set; }
        public bool? isApproved { get; set; }
        public string punchCardNo { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
    }
}
