using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.SalaryProcess
{
    public class SalaryEmployeeStructureHistory : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int structureHistoryId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? salarySlabId { get; set; }
        public SalarySlab salarySlab { get; set; }
        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }
        public decimal? historyAmount { get; set; }       
        public DateTime? effectiveDate { get; set; }        
    }
}
