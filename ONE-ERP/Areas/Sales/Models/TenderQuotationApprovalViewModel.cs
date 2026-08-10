using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class TenderQuotationApprovalViewModel
    {
        public int quotationMasterId { get; set; }
        public bool? isSelect { get; set; }
        public int? approvalStatusValue { get; set; }
        public List<TenderQuotationApprovalViewModel> lstMasterViewModel { get; set; }
    }
    
}
