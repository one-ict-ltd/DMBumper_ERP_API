using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalProductMonitor : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int monitorId { get; set; }
        public string territoryCode { get; set; }
        //public CmnTerritorys territory { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}
