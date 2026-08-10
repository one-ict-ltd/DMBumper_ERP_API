using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductWiseSpecification : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productWiseSpecificationId { get; set; }
        public int? productId { get; set; }
        public InvProduct product { get; set; }
        [MaxLength(250)]
        public string skuName { get; set; }
        [MaxLength(50)]
        public string skuNumber { get; set; }
        public string imageUrl { get; set; }
        public bool? hasSet { get; set; }
        public bool? holdSales { get; set; }
        public string salesHeldbatchNumber { get; set; }
        public int? showOrderNumber { get; set; }
        public int? specWiseUomId { get; set; }
        public int? finishgoodCategoryId { get; set; }
    }
    public class InvProductSpecListExcludedFromReports : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
    }
}
