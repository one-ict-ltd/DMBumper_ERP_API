using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdProductGroupAssign : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prdGroupAssignId { get; set; }

        public int? phGroupMasterId { get; set; }
        public PrdProcessHeadGroupMaster phGroupMaster { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public string remarks { get; set; }
        public int? shortOrder { get; set; }

    }
}
