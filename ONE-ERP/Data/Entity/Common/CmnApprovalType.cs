using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnApprovalType: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int approvalTypeId { get; set; }        
        [MaxLength(250)]
        public string approvalTypeName { get; set; }        
    }
}
