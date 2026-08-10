//using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductWiseUOMConverter : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uomConvertId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? fromQty { get; set; }
        public int? fromUomId { get; set; }
        //public InvProductUOM fromUom { get; set; }
        public decimal? toQty { get; set; }
        public int? toUomId { get; set; }
        //public InvProductUOM toUom { get; set; }
    }
}
