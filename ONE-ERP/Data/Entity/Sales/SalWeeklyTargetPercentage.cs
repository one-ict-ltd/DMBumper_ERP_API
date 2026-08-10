using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalWeeklyTargetPercentage : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int weeklyTargetId { get; set; }
        public int weekNo { get; set; }
        public DateTime? wStartDate { get; set; }
        public DateTime? wEndDate { get; set; }
        public decimal? percentage { get; set; }

        /*
        public string territoryCode { get; set; }
        public CmnTerritorys territory { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        */
    }
}
