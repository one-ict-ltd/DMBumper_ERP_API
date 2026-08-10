using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class PurchaseApprovalMatrixViewModel
    {
        public int? approvalMatrixId { get; set; }
        public int? employeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public int? approverId { get; set; }
        public string approverName { get; set; }
        public string approverCode { get; set; }
        public int? departmentId { get; set; }
        public string departmentName { get; set; }
        public int? seqNo { get; set; }
        public bool? isFinalApproval { get; set; }
        public bool? isActive { get; set; }

        public List<PurchaseApprovalMatrixViewModel> lstDetails { get; set; }
        public int? productTypeId { get; set; }
    }
}
