using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeDesignation : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeDesignationId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? designationId { get; set; }
        public HrmDesignation designaton { get; set; }
        public DateTime? effectiveDate { get; set; }
    }
}
