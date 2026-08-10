using System;
namespace ONEERP.Areas.Sales.Models
{
    public class MIODailySalesForecastViewModel
    {
        public int? salesForecastId { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public int? noOfOrder { get; set; }
        public decimal? orderValue { get; set; }
        public DateTime? orderDate { get; set; }
    }
}
