using ONEERP.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class BillPaymentsViewModel
    {
        public int? paymentMasterId { get; set; }
        public string paymentNumber { get; set; }
        public string voucherRemarks { get; set; }
        public string billNo { get; set; }
        public int? partyId { get; set; }
        public int? accountId { get; set; }
        public int? billMasterId { get; set; }
        public DateTime? paymentDate { get; set; }
        public DateTime? voucherDate { get; set; }
        public decimal? netPaymentAmount { get; set; }
        public decimal? paymentAmount { get; set; }
        public decimal? vatPaymentAmount { get; set; }
        public decimal? vdsPercent { get; set; }
        public decimal? tdsPercent { get; set; }
        public decimal? vdsPaymentAmount { get; set; }
        public decimal? tdsPaymentAmount { get; set; }
        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        public string trxNo { get; set; }

        //public List<VoucherDetailViewModel> lstdetailmodel { get; set; } = new List<VoucherDetailViewModel>();
        public List<BillPaymentsDetailsViewModel> lstDetailsViewModel { get; set; }
    }

    public class BillPaymentsDetailsViewModel
    {
        public int paymentMasterId { get; set; }
        public string paymentNumber { get; set; }
        public string referenceNo { get; set; }
        public int? partyId { get; set; }
        public int? billMasterId { get; set; }
        public DateTime? paymentDate { get; set; }
        public decimal? paymentAmount { get; set; }
        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        public string trxNo { get; set; }
        public int? voucherMasterId { get; set; }
        public bool? isSelect { get; set; }
        public bool? isActive { get; set; }
    }

}
