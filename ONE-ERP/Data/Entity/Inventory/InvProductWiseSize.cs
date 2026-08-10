using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductWiseSize:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productWiseSizeId { get; set; }
        public int? sizeId { get; set; }
        public InvProductSize size { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        public bool? isDefault { get; set; }
    }
}
