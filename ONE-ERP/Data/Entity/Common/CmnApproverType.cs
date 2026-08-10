using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnApproverType: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int approverTypeId { get; set; }
        public int? approvalTypeId { get; set; }
        public CmnApprovalType approvalType { get; set; }
        [MaxLength(250)]
        public string approverTypeName { get; set; }        
        public string approverEmpId { get; set; }
    }
}
