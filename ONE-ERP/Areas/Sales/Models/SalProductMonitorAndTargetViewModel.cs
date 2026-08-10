using System;
using System.Collections.Generic;
using ONEERP.Data.Entity.Inventory;

namespace ONEERP.Areas.Sales.Models
{
    public class SalProductMonitorViewModel
    {
        public int monitorId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public List<SalProductMonitorViewModel> lstProductMonitor { get; set; }
    }
    public class SalWeeklyTargetPercentage
    {
        public int weeklyTargetId { get; set; }
        public int weekNo { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public decimal? tgPercent { get; set; }

        public List<SalWeeklyTargetPercentage> lstWeeklyTargetPercentage { get; set; }
    }
}
