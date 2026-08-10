using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalMIODailySalesForecast : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesForecastId { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public int? noOfOrder { get; set; }
        public decimal? orderValue { get; set; }
        public DateTime? orderDate { get; set; }

    }
}
