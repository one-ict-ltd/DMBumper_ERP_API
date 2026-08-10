using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductCategory:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productCategoryId { get; set; }
        [MaxLength(250)]
        public string categoryName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
        public bool? hasSerialNo { get; set; }
        public int? serialNo { get; set; }
        public int? animalLedgerId { get; set; }
        public int? humanLedgerId { get; set; }

    }
}
