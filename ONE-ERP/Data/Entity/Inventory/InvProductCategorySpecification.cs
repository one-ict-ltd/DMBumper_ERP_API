using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductCategorySpecification:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productCategorySpecificationId { get; set; }
        public int? productCategoryId { get; set; }
        public InvProductCategory productCategory { get; set; }
        [MaxLength(250)]
        public string specificationType { get; set; }


    }
}
