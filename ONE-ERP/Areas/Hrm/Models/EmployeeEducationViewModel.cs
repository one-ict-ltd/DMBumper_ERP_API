using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeEducationViewModel
    {
        public int? educationalQualificationId { get; set; }
        public int? employeeId { get; set; }
        public string institution { get; set; }
        public int? resultId { get; set; }
        public string majorGroup { get; set; }
        public string grade { get; set; }
        public int? passingYear { get; set; }
        public int? degreeId { get; set; }
        public int? degreesubjectId { get; set; }
        public int? educationOrganizationId { get; set; }
        public string certificateUrl { get; set; }
        public bool? isActive { get; set; } = true;
    }
}
