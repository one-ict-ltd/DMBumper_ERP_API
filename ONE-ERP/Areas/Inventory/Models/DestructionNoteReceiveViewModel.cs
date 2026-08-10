using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Inventory.Models
{
    public class DestructionNoteReceiveViewModel
    {
        public int destructionNoteReceiveId { get; set; }
        public int damageExpireProductReturnMasterId { get; set; }
        public string destructionNoteReceiveNo { get; set; }
        public DateTime? destructionNoteReceiveDate { get; set; }
        public int? miscellaneousTypeId { get; set; } //1 =Damage 6 =Expire
        public string remarks { get; set; }
        public int? isApproved { get; set; }
        public string MarketOrDepo { get; set; }
        public List<DestructionNoteReceiveDetailViewModel> lstDetailsViewModel { get; set; }
    }

    public class DestructionNoteReceiveDetailViewModel
    {
        public int destructionNoteRecvDetailId { get; set; }
        public int? destructionNoteReceiveId { get; set; }
        public int damageExpireProductReturnDetailId { get; set; }
        public int? MiscellaneousItemDetailId { get; set; }
        public int? productSpecificationId { get; set; }
        public decimal? qty { get; set; }
    }
}
