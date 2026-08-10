using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductSetDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productSetDetailsId { get; set; }
        public int? productSetMasterId { get; set; }
        public InvProductSetMaster productSetMaster { get; set; }
        public int? accessories_ProductWiseSpecificationId { get; set; }
        public InvProductWiseSpecification accessories_ProductWiseSpecification { get; set; }
        public decimal? qty { get; set; }
    }
}
