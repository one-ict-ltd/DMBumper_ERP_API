using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeMobileBill:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeMobileBillId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public string mobile { get; set; }
        public decimal? billLimit { get; set; }
        public decimal? actualBill { get; set; }
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public string remarks { get; set; }
    }
}
