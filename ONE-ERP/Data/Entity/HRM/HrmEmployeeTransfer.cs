using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeTransfer : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeTransferId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public DateTime? transferDate { get; set; }
        public int? HrmSalaryLocationId { get; set; }
        public HrmSalaryLocation HrmSalaryLocation { get; set; }
        public int? HrmNewSalaryLocationId { get; set; }
        public HrmSalaryLocation HrmNewSalaryLocation { get; set; }
        public decimal? grossSalary { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
    }
}
