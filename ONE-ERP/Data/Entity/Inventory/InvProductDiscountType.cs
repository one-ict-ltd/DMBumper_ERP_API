using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductDiscountType:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int discountTypeId { get; set; }
        [MaxLength(250)]
        public string discountTypeName { get; set; }
        [MaxLength(50)]
        public string discountTypeCode { get; set; }
        [MaxLength(10)]
        public string aliasName { get; set; }
    }
}
