using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductDiscount:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int discountId { get; set; }
        public int? discountTypeId { get; set; }
        public InvProductDiscountType productDiscountType { get; set; }
        public int? productId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public decimal? discountAmountOrPercentage { get; set; }
        public bool? isAmount { get; set; }
    }
}
