using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductModel:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int modelId { get; set; }
        [MaxLength(50)]
        public string modelName { get; set; }
        [MaxLength(50)]
        public string modelCode { get; set; }
        [MaxLength(10)]
        public string aliasName { get; set; }
    }
}
