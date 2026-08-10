using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalDMSReportMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reportMasterId { get; set; }
        public string masterReportName { get; set; }
      //  public string masterReportTypeId { get; set; } // RSM//MIO//ZONE
        public int? sortOrder { get; set; }
    }
}
