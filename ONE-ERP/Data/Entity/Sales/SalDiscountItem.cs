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
    public class SalDiscountItem:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountItemId { get; set; }
        public int? bonusforSpecificationId { get; set; }
        public InvProductWiseSpecification bonusforSpecification { get; set; }
        public decimal? forQuantity { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? bonusSpecificationId { get; set; }
        public InvProductWiseSpecification bonusSpecification { get; set; }
        public decimal? quantity { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
