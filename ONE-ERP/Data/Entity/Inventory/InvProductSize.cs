using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductSize:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sizeId { get; set; }
        public decimal? size { get; set; }
        public int? uomId { get; set; }
        [MaxLength(250)]
        public string uomName { get; set; }
    }
}
