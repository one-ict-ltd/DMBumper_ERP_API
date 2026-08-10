using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductSubCategory:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productSubCategoryId { get; set; }
        [MaxLength(250)]
        public string subCategoryName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
        public int? productCategoryId { get; set; }
        public InvProductCategory productCategory { get; set; }
        public int? parentId { get; set; }
    }
}
