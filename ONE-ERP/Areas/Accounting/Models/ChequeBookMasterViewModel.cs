using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class ChequeBookMasterViewModel
    {
        public int? chequeBookMasterId { get; set; }
        public string chequeBookId { get; set; }
        public string bankName { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public int? chequeNumberCurrent { get; set; }
        public int? chequeNumberStarting { get; set; }
        public DateTime? chequeDate { get; set; }
        public decimal? chequeAmount { get; set; }
        public bool? isAccountPayee { get; set; }
        public bool? isBearer { get; set; }
        public bool? isNonNegotiable { get; set; }
        public bool? isPayableOndateOnly { get; set; }
        public bool? isVoid { get; set; }
        public bool? isPrinted { get; set; }
        public bool? isCleared { get; set; }
        public bool? isWithoutDate { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }

        public List<ChequeBookDetailsViewModel> lstdetailCheque { get; set; }

    }
}
