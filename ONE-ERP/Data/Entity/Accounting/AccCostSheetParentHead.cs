using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCostSheetParentHead:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int parentHeadId { get; set; } 
        [MaxLength(250)]
        public string parentHeadName { get; set; }
        [MaxLength(50)]
        public string shortName { get; set; }
        public int? sortOrder { get; set; }
    }
}
