using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductBrand :NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brandId { get; set; }
        [MaxLength(250)]
        public string brandName { get; set; }
        [MaxLength(50)]
        public string brandCode { get; set; }
        [MaxLength(50)]
        public string aliasName { get; set; }
    }
}
