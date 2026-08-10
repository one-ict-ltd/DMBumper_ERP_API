using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Accounting.Models
{
    public class VoucherMasterViewModel
    {
        public int? voucherMasterId { get; set; }
        public DateTime? voucherDate { get; set; }
        public string refNo { get; set; }
        public string voucherNo { get; set; }
        public int? voucherTypeId { get; set; }
        public string remarks { get; set; }
        public string editRemarks { get; set; }
        public int? isPosted { get; set; }
        public decimal? voucherAmount { get; set; }
        public int? fundSourceId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
        public bool? isCheque { get; set; }
        public string ChequeNo { get; set; }
     
        public List<VoucherDetailViewModel> lstdetailmodel { get; set; } = new List<VoucherDetailViewModel>();
        public List<CostCentreAllocationViewModel> lstcostmodel { get; set; } = new List<CostCentreAllocationViewModel>();
        public List<VoucherApprovalLogViewModel> lstappmodel { get; set; } = new List<VoucherApprovalLogViewModel>();
        public List<VoucherAttachmentlViewModel> voucherAttachmentList { get; set; } = new List<VoucherAttachmentlViewModel>();
    }

    public class VoucherPostingViewModel
    {
        public int? voucherMasterId { get; set; }
        public DateTime? voucherDate { get; set; }
        public string refNo { get; set; }
        public int? voucherTypeId { get; set; }
        public string remarks { get; set; }
        public int? isPosted { get; set; }
        public decimal? voucherAmount { get; set; }
        public int? fundSourceId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public bool? isCheque { get; set; }
        public string comments { get; set; }

        public List<VoucherPostingViewModel> lstMasterViewModel { get; set; }
    }

    public class VoucherMasterViewModelExcel
    {
        public int? voucherMasterId { get; set; }
        public DateTime? voucherDate { get; set; }
        public string refNo { get; set; }
        public int? voucherTypeId { get; set; }
        public string remarks { get; set; }
        public int? isPosted { get; set; } = 0;
        public decimal? voucherAmount { get; set; }
        public int? fundSourceId { get; set; }
        public int? companyId { get; set; } = 1;
        public int? sbuId { get; set; } = 1;
        public bool? isActive { get; set; }
        public bool? isCheque { get; set; }
        public string ChequeNo { get; set; }

        public List<VoucherDetailViewModelExcel> lstMaster { get; set; }
    }

    public class VoucherDetailViewModelExcel
    {
        public string accountCode { get; set; }
        public int? ledgerId { get; set; }
        public string party { get; set; }
        public int? partyId { get; set; }
        public string costCentre { get; set; }
        public int? costCentreId { get; set; }
        public Decimal? drAmount { get; set; }
        public Decimal? crAmount { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
        public string accountName { get; set; }
    }

    public class VoucherAttachmentlViewModel
    {
        public int voucherAttachmentId { get; set; }
        public string fileName { get; set; }
        public string remarks { get; set; }
        public string fileString { get; set; }
        public string attachmentUrl { get; set; }
        public string ext { get; set; }
    }
}
