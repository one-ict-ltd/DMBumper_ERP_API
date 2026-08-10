using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class InvRePackProductTransferViewModel
    {
        public int RePackProductTransferId { get; set; }
        public int destructionNoteReceiveId { get; set; }
        public string RePackProductTransferNo { get; set; }
        public DateTime? RePackProductTransferDate { get; set; }
        public int? miscellaneousTypeId { get; set; } //1 =Damage 6 =Expire
        public string remarks { get; set; }
        public int? isApproved { get; set; }
        public string MarketOrDepo { get; set; }
        public List<InvRePackProductTransferDetailViewModel> lstDetailsViewModel { get; set; }
    }
    public class InvRePackProductTransferDetailViewModel
    {
        public int RePackProductTransferDetailId { get; set; }
        public int? RePackProductTransferId { get; set; }
        public int destructionNoteRecvDetailId { get; set; }
        public int? productSpecificationId { get; set; }
        public string batchNo { get; set; }
        public decimal? transferQty { get; set; }
    }
}
