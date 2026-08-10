using ONEERP.Areas.Hrm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//
using System.Linq;
using System.Threading.Tasks;
//

namespace ONEERP.Areas.Hrm.Models
{
    public class AwardViewModel
    {
        public string employeeID { get; set; }

        public string awardId { get; set; }

        [Required]
        [Display(Name = "Award Name")]
        public string awardName { get; set; }

        [Display(Name = "Perpose")]
        public string perpose { get; set; }

        [Display(Name = "Date")]
        public DateTime? txtAwardDate { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public PhotographViewModel photograph { get; set; }
        public EmployeeViewModel employeeInfo { get; set; }

        //public Award fLang { get; set; }

        public IEnumerable<AwardViewModel> awards { get; set; }
    }
}
