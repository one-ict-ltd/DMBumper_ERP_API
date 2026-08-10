using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class EducationalSubjectViewModel
    {
        public int subjectId { get; set; }
        public string name { get; set; }
        //public string shortName { get; set; }
        public bool isActive { get; set; }
    }
}
