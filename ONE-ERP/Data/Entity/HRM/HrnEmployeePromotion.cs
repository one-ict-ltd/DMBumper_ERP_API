using ONEERP.Data.Entity.HrmMaster;
using ONEERP.Data.Entity.Salary.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrnEmployeePromotion:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeePromotionId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public DateTime? promotionDate { get; set; }
        public int? HrmSalaryLocationId { get; set; }
        public HrmSalaryLocation HrmSalaryLocation { get; set; }
        public int? HrmNewSalaryLocationId { get; set; }
        public HrmSalaryLocation HrmNewSalaryLocation { get; set; }
        public int? SalarySlabId { get; set; }
        public SalarySlab SalarySlab { get; set; }
        public int? NewSalarySlabId { get; set; }
        public SalarySlab NewSalarySlab { get; set; }
        public string previousDesignation { get; set; }
        public string currentDesignation { get; set; }
        public string previousDepartment { get; set; }
        public string currentDepartment { get; set; }
        public decimal? PreviousGrossSalary { get; set; }
        public decimal? NewGrossSalary { get; set; }
        public decimal? incrementSalary { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public string type { get; set; }
    }
}
