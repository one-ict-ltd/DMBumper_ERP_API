using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductWiseUOM:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productWiseUOMId { get; set; }
        public int? uomId { get; set; }
        public InvProductUOM productUOM { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }

    }
}
