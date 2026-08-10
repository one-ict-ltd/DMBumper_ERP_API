using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class AutoVoucherMasterViewModel
    {
        public int? autoVoucherMasterId { get; set; }
        public int? autoVoucherNameId { get; set; }
        public int? voucherTypeId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public string description { get; set; }              
        public bool? isActive { get; set; }
        public List<AutoVoucherDetailViewModel> lstDetails { get; set; }
    }
}
