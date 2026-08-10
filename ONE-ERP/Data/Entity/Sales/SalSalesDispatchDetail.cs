using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesDispatchDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dispatchDetailId { get; set; }

        public int? SalPickingMasterId { get; set; }
        public SalPickingMaster SalPickingMaster { get; set; }

        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salesInvoice { get; set; }
        public int? salSalesDispatchMasterId { get; set; }
        public SalSalesDispatchMaster salSalesDispatchMaster { get; set; }

    }
}
