using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesGrossRetun:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesGrossRetunId { get; set; }
        public string grossReturnNumber { get; set; }
        public DateTime? grossReturnDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }

        public int? salSalesGrossReturnMasterId { get; set; }
        public SalSalesGrossReturnMaster salSalesGrossReturnMaster { get; set; }

        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salSalesInvoice { get; set; }
        public decimal? Qty { get; set; }
        public decimal? amount { get; set; }
        public string batchNo { get; set; }
    }
}
