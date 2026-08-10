using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class BillViewModel
    {
        public int? billMasterId { get; set; }
        public string billNo { get; set; }
        public DateTime? billDate { get; set; }
        public int? partyId { get; set; }
        public string omrRmrNo { get; set; }
        public DateTime? omrRmrDate { get; set; }
        public string supplierBillNo { get; set; }
        public DateTime? supplierBillDate { get; set; }
        public string supplierChallanNo { get; set; }
        public DateTime? supplierChallanDate { get; set; }

        public DateTime? maturityDate { get; set; }
        public int? creditPeriod { get; set; }
        public string particular { get; set; }
        public string remarks { get; set; }
        public decimal? grandTotal { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? discountAmount { get; set; }
        public decimal? truckFair { get; set; }
        public decimal? transportBill { get; set; }
        public decimal? tdsPercent { get; set; }
        public decimal? tdsAmount { get; set; }
        public decimal? netAmount { get; set; }
        public decimal? advancePaidAmount { get; set; }
        public int? billStatus { get; set; }
        public List<BillDetailsViewModel> lstDetailsViewModel { get; set; }
    }

    public class BillDetailsViewModel
    {
        public int billDetailId { get; set; }
        public int? billMasterId { get; set; }
        public int? grnDetailId { get; set; }
        public decimal? receivedQty { get; set; }
        public decimal? rate { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? actualAmount { get; set; }
        public bool? isSelect { get; set; }
        public bool? isActive { get; set; }
    }

}
