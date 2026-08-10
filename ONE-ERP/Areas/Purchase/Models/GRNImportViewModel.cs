using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class GRNImportViewModel
    {
        public int? ImpgrnMasterId { get; set; }
        public int? ImpPreLCInfoMasterId { get; set; }
        public int? partyId { get; set; }
        public DateTime? grnDate { get; set; }
        public string grnNo { get; set; }
        public DateTime? factoryReceivedDate { get; set; }
        public bool? isActive { get; set; }
        public int? grnStatus { get; set; }
        public string remarks { get; set; }
        public string RMRNo { get; set; }
        public string MRRNo { get; set; }
        public string TruckNo { get; set; }
        public string DriverName { get; set; }
        public string CFAgentName { get; set; }
        public string mobileNo { get; set; }
        public string rejectedGRN { get; set; }
        public List<GRNImportDetailsViewModel> lstDetailsViewModel { get; set; }
    }

    public class GRNImportDetailsViewModel
    {
        public int? grnDetailsId { get; set; }
        public int? PurImpPreLCInfoDetailId { get; set; }
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
        public int noOfBag { get; set; }
        public string batchNo { get; set; }
        public string manufactureOrigin { get; set; }
        public string QtyWithPackSize { get; set; }
        public string PrevQcReferenceNo { get; set; }
    }

}
