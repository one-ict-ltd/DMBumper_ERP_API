using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductSupplier:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productsupplierId { get; set; }
        public int? supplierId { get; set; }
        public AccParty party { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
    }
}
