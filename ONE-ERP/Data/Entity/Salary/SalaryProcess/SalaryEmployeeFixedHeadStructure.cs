using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.SalaryProcess
{
    public class SalaryEmployeeFixedHeadStructure : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpFixedHeadStructureId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }
        public decimal? structureAmount { get; set; }
    }
}
