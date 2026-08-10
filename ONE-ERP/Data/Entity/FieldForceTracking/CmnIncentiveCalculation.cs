using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnIncentiveCalculation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncentiveCalculationID { get; set; }
        public decimal? achivementPercentage { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public decimal? targetBudget { get; set; }
        public decimal? achivementTargetBudget { get; set; }
        public decimal? superstarValueSales { get; set; }
        public decimal? incentiveAmount { get; set; }
        public bool? isActive { get; set; }
        [DefaultValue(0)]
        public bool? isDelete { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        [MaxLength(250)]
        public string createdBy { get; set; }
        [MaxLength(250)]
        public string updatedBy { get; set; }
    }
}
