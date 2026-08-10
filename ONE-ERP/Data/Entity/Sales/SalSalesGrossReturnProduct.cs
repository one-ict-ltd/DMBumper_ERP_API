using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesGrossReturnProduct:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesGrossRetunProductId { get; set; }

        public int? salSalesGrossReturnMasterId { get; set; }
        public SalSalesGrossReturnMaster salSalesGrossReturnMaster { get; set; }

        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? toUomId { get; set; }
        public decimal? returnQty { get; set; }
        public decimal? CtnQty { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? amount { get; set; }
        public decimal? price { get; set; }
        public decimal? tp { get; set; }
        public decimal? vat { get; set; }
        public decimal? discount { get; set; }
        public decimal? totalPrice { get; set; }
        public string batchNo { get; set; }
    }
}
