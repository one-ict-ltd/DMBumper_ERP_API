using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class GRNViewModel
    {
        public int? grnMasterId { get; set; }
        public int? purchaseOrderId { get; set; }
        public int? partyId { get; set; }
        public DateTime? grnDate { get; set; }
        public string grnNo { get; set; }
        public string inhouseChallanNo { get; set; }
        public string factoryReceiveSINo { get; set; }
        public string supplierChallanNo { get; set; }
        public DateTime? supplierChallanDate { get; set; }
        public DateTime? factoryReceivedDate { get; set; }
        public bool? isActive { get; set; }
        public int? grnStatus { get; set; }
        public string remarks { get; set; }
        public string rejectedGRN { get; set; }

        public List<GRNDetailsViewModel> lstDetailsViewModel { get; set; }

    }

    public class GRNDetailsViewModel
    {
        public int? grnDetailsId { get; set; }
        public int? purchaseOrderDetailsId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? receivedQty { get; set; }
        public decimal? price { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? actualAmount { get; set; }
        public bool? isSelect { get; set; }
        public bool? isActive { get; set; }
        public decimal? actualRcvQty { get; set; }
        public int? toUOMId { get; set; }
        public DateTime? mfgDate { get; set; }
        public DateTime? expiryDate { get; set; }
        public int? noOfBag { get; set; }
        public string batchNo { get; set; }
        public string manufactureOrigin { get; set; }
        public string QtyWithPackSize { get; set; }
        public string PrevQcReferenceNo { get; set; }
    }

}
