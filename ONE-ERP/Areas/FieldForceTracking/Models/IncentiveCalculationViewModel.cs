using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class IncentiveCalculationViewModel
    {
        public int IncentiveCalculationID { get; set; }
        public decimal? achivementPercentage { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public decimal? targetBudget { get; set; }
        public decimal? achivementTargetBudget { get; set; }
        public decimal? superstarValueSales { get; set; }
        public decimal? incentiveAmount { get; set; }
        public bool? isActive { get; set; }
    }
}
