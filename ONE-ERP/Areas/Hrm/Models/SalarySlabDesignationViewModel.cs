using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class SalarySlabDesignationViewModel
    {
        public int? slabDesignationId { get; set; } = 0;
        public int? salaryGradeId { get; set; }
        // public string salaryGradeName { get; set; }
        public int? salarySlabId { get; set; }
        //public string salarySlabName { get; set; }
        public int? designationId { get; set; }
        // public string designationName { get; set; }
    }
}
