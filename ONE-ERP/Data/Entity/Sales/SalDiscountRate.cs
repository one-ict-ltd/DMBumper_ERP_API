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
    public class SalDiscountRate:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountRateId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public decimal? price { get; set; }
        public string discountType { get; set; }
        public decimal? percentAmount { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? amount { get; set; } // final amount
        public DateTime? fromDate { get; set; }
        public DateTime? endDate { get; set; }


    }
}
