using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeExperience:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeExperienceId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public string organization { get; set; }
        public string appointedDesignation { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string remarks { get; set; }
    }
}
