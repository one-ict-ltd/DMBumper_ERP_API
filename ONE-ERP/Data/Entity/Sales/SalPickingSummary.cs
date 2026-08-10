using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalPickingSummary : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pickingSummaryId { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salSalesInvoice { get; set; }
        public int? pickingMasterId { get; set; }
        public SalPickingMaster pickingMaster { get; set; }

    }
}
