using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeAttachmentUploadViewModel
    {
        public int employeeAttachmentId { get; set; }
        public int employeeId { get; set; }
        public string ImageUrl { get; set; }
        public string extension { get; set; }
        public string tempImageUrl { get; set; }
    }
}
