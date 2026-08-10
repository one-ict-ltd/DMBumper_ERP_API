using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvCategoryProductWiseSpecificationMapping : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryProductWiseSpecificationMappingId { get; set; }
        [MaxLength(500)]
        public string categoryName { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? serialNo { get; set; }
        

    }
}
