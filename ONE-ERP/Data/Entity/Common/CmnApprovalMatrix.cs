using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnApprovalMatrix : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int approvalMatrixId { get; set; }
        public int? approvalTypeId { get; set; }
        public CmnApprovalType approvalType { get; set; }
        public int? approverTypeId { get; set; }
        public CmnApproverType approverType { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? nextApproverId { get; set; }
        public int? sequenceNo { get; set; }
        [MaxLength(300)]
        public string matrixName { get; set; }

        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? productTypeId { get; set; }
        public InvProductType productType { get; set; }
        public int? isFinalApproval { get; set; }
    }
}
