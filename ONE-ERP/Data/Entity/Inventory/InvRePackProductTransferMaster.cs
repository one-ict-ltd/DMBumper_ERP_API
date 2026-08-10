using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvRePackProductTransferMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RePackProductTransferId { get; set; }
        public int destructionNoteReceiveId { get; set; }
        public string RePackProductTransferNo { get; set; }
        public DateTime? RePackProductTransferDate { get; set; }
        public int? miscellaneousTypeId { get; set; } //1 =Damage 6 =Expire
        public string remarks { get; set; }
        public int? isApproved { get; set; }
        public string MarketOrDepo { get; set; }

        public bool? isDestroy { get; set; }
    }
}
