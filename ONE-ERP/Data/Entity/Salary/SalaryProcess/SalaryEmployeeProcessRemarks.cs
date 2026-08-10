using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.SalaryProcess
{   
    public class SalaryEmployeeProcessRemarks : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeProcessRemarksId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public string comments { get; set; }
        public decimal? revisedAmount { get; set; }
    }
}
