using System;

namespace ONEERP.Areas.Salary.Models
{
    public class SalaryEmployeeStructureViewModel
    {
        public int employeeStructureId { get; set; }
        public int? employeeId { get; set; }
        public int? salarySlabId { get; set; }
        public int? salaryHeadId { get; set; }
        public decimal slabAmount { get; set; }
        public decimal structureAmount { get; set; }
        public DateTime? effectiveDate { get; set; }
        public bool? isActive { get; set; }
        public decimal bankAmount { get; set; }
        public decimal cashAmount { get; set; }
        public int salaryGradeId { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
    }
}
