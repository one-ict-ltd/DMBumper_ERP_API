using ONEERP.Data.Entity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductSetMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productSetMasterId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company{ get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        [MaxLength(250)]
        public string ProductSetName { get; set; }
        public int? master_ProductWiseSpecificationId { get; set; }
        public InvProductWiseSpecification master_ProductWiseSpecification { get; set; }
    }
}
