using System.Collections.Generic;

namespace ONEERP.Areas.Hrm.Models
{
    public class EmployeeJobDescriptionViewModel
    {
        public int employeeJobDescriptionId { get; set; }
        public int? employeeId { get; set; }
        public int? slNo { get; set; }
        public string jobDescription { get; set; }
        public bool? isActive { get; set; }
        public List<EmployeeJobDescriptionViewModel> lstDetails { get; set; }
        
    }
}
