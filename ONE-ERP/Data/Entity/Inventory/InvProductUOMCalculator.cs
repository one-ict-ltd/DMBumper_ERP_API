using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductUOMCalculator:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uomCalculatorId { get; set; }

        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }

        public int? productUOMId { get; set; }
        public InvProductUOM productUOM { get; set; }
    }
}
